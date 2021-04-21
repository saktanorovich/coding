#include <cstdio>
#include <cstring>
#include <iostream>
#include <sstream>
#include <string>
using namespace std;

int const max_pages = 250;
int const max_words_in_page = 8;
int const max_word_weight = max_words_in_page;
int const max_word_length = 20;
int const alphabet_size = 26;
int const top_pages = 5;

struct list_of_pages {
      int page;
      int word_weight;
      list_of_pages *next;
      list_of_pages(int page, int word_weight, list_of_pages *next) {
            this->page = page;
            this->word_weight = word_weight;
            this->next = next;
      }
};

int toint(char c) {
      if ('A' <= c && c <= 'Z') {
            return c - 'A';
      }
      return (c - 'a');
}

class trie {
private:
      trie* _children[alphabet_size];
      list_of_pages *_pages;
public:
      trie() {
            for (int i = 0; i < alphabet_size; ++i) {
                  _children[i] = 0;
            }
            _pages = 0;
      }
      void add_word(char word[max_word_length], int from_page, int weight_in_page) {
            trie* x = this;
            for (; *word; ++word) {
                  int c = toint(*word);
                  if (x->_children[c] == 0) {
                        x->_children[c] = new trie();
                  }
                  x = x->_children[c];
            }
            x->_pages = new list_of_pages(from_page, weight_in_page, x->_pages);
      }
      list_of_pages* walk(char word[max_word_length]) {
            trie* x = this;
            for (; *word; ++word) {
                  x = x->_children[toint(*word)];
                  if (x == 0) {
                        return 0;
                  }
            }
            return x->_pages;
      }
};

trie *_trie = new trie();
char word[max_word_length];
int strength[max_pages];
char buffer[2048];

int main() {
      freopen("input.txt", "r", stdin);
      freopen("output.txt", "w", stdout);

      int query_id = 0, pages_count = 0, word_weight;
      printf("Query Pages\n");
      while (1) {
            cin.getline(buffer, 2048);
            istringstream ss(buffer);
            ss >> word;
            if (strcmp(word, "P") == 0) {
                  ++pages_count;
                  word_weight = max_word_weight;
                  while (ss >> word) {
                        _trie->add_word(word, pages_count - 1, word_weight);
                        --word_weight;
                  }
                  continue;
            }
            if (strcmp(word, "Q") == 0) {
                  printf("Q%d:", ++query_id);
                  for (int j = 0; j < max_pages; ++j) {
                        strength[j] = 0;
                  }
                  word_weight = max_word_weight;
                  while (ss >> word) {
                        for (list_of_pages *p = _trie->walk(word); p != 0; p = p->next) {
                              strength[p->page] += p->word_weight * word_weight;
                        }
                        --word_weight;
                  }
                  for (int t = 0, f = 1; t < top_pages; ++t) {
                        int any = 0;
                        for (int j = 1; j < max_pages; ++j) {
                              if (strength[j] > strength[any]) {
                                    any = j;
                              }
                        }
                        if (strength[any] > 0) {
                              if (f) {
                                    if (query_id < 10)  printf(" ");
                                    if (query_id < 100) printf(" ");
                                    f = 0;
                              }
                              printf(" P%d", any + 1);
                              strength[any] = 0;
                        }
                        else {
                              break;
                        }
                  }
                  printf("\n");
                  continue;
            }
            if (strcmp(word, "E") == 0) {
                  break;
            }
      }
      return 0;
}
