#include <cstdio>
#include <cstring>
#include <iostream>
#include <stack>
#include <vector>
using namespace std;

struct SyntaxNode {
public:
    vector<SyntaxNode*> children;
    char element;
    int numOfCommands;
    int numOfOperands;
public:
    SyntaxNode(char element) {
        this->element = element;
        if (isalpha(element)) {
            numOfOperands = 1;
            numOfCommands = 0;
        } else {
            numOfCommands = 1;
            numOfOperands = 0;
        }
    }
    void add(SyntaxNode* child) {
        children.push_back(child);
        numOfCommands += child->numOfCommands;
        numOfOperands += child->numOfOperands;
    }
};

struct SyntaxTree {
public:
    SyntaxNode* root;
public:
    SyntaxTree(SyntaxNode* root) {
        this->root = root;
    }
public:
    static SyntaxTree* parse(char *s, int n) {
        stack<SyntaxNode*> stack;
        for (int i = 0; i < n; ++i) {
            SyntaxNode* t = new SyntaxNode(s[i]);
            if (isalpha(s[i])) {
                stack.push(t);
                continue;
            }
            if (s[i] == '@') {
                t->add(stack.top());
                stack.pop();
                stack.push(t);
            } else {
                SyntaxNode* r = stack.top(); stack.pop();
                SyntaxNode* l = stack.top(); stack.pop();
                t->add(l);
                t->add(r);
                stack.push(t);
            }
        }
        return new SyntaxTree(stack.top());
    }
};

class Assembly {
private:
    int memory[256];
public:
    Assembly() {
        for (int i = 0; i < 256; ++i) {
            memory[i] = 0;
        }
    }
public:
    void emit(SyntaxTree* syntax) {
        emit(syntax->root);
    }
private:
    void emit(SyntaxNode* node) {
        if (isalpha(node->element)) {
            printf("L %c\n", node->element);
            return;
        }
        if (node->element == '@') {
            emit(node->children[0]);
            printf("N\n");
            return;
        }
        SyntaxNode* l = node->children[0];
        SyntaxNode* r = node->children[1];
        emit(l);
        if (node->element == '+') {
            if (r->numOfCommands > 0) {
                int memIdx = alloc();
                printf("ST $%d\n", memIdx);
                emit(r);
                printf("A $%d\n", memIdx);
                free(memIdx);
            } else {
                printf("A %c\n", r->element);
            }
        } else if (node->element == '*') {
            if (r->numOfCommands > 0) {
                int memIdx = alloc();
                printf("ST $%d\n", memIdx);
                emit(r);
                printf("M $%d\n", memIdx);
                free(memIdx);
            } else {
                printf("M %c\n", r->element);
            }
        } else if (node->element == '-') {
            if (r->numOfCommands > 0) {
                int memIdx = alloc();
                printf("ST $%d\n", memIdx);
                emit(r);
                printf("N\n");
                printf("A $%d\n", memIdx);
                free(memIdx);
            } else {
                printf("S %c\n", r->element);
            }
        } else if (node->element == '/') {
            if (r->numOfCommands > 0) {
                int memIdxL = alloc();
                printf("ST $%d\n", memIdxL);
                emit(r);
                int memIdxR = alloc();
                printf("ST $%d\n", memIdxR);
                printf("L $%d\n", memIdxL);
                printf("D $%d\n", memIdxR);
                free(memIdxR);
                free(memIdxL);
            } else {
                printf("D %c\n", r->element);
            }
        }
    }

    int alloc() {
        for (int i = 0; i < 256; ++i) {
            if (memory[i] == 0) {
                memory[i] = 1;
                return i;
            }
        }
        return -1;
    }

    void free(int memIdx) {
        memory[memIdx] = 0;
    }
};

int main() {
    char s[1024];
    for (int t = 1; scanf("%s", s) == 1; ++t) {
        if (t > 1) {
            printf("\n");
        }
        (new Assembly())->emit(SyntaxTree::parse(s, strlen(s)));
    }
    return 0;
}