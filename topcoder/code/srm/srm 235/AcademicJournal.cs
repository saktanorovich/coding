using System;
using System.Collections.Generic;

    class AcademicJournal
    {
        private class Journal
        {
            public string name;
            public int iPapers;
            public int iRefs;
            public Journal(string name)
            {
                this.name = name;
                iPapers = iRefs = 0;
            }
        }
        private bool more(Journal j1, Journal j2)
        {
            if (j1.iRefs * j2.iPapers > j2.iRefs * j1.iPapers)
            {
                return true;
            }
            else if (j1.iRefs * j2.iPapers == j2.iRefs * j1.iPapers)
            {
                if (j1.iPapers > j2.iPapers)
                {
                    return true;
                }
                else if (j1.iPapers == j2.iPapers)
                {
                    if (j1.name.CompareTo(j2.name) < 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string[] rankByImpact(string[] papers)
        {
            Dictionary<string, Journal> map = new Dictionary<string, Journal>();
            string[] journalNames = new string[papers.Length];
            for (int i = 0; i < papers.Length; ++i)
            {
                string[] parts = papers[i].Split('.');
                string name = parts[0];
                
                if (!map.ContainsKey(name))
                {
                    map[name] = new Journal(name);
                }
                Journal journal = map[name];
                journal.iPapers++;
                map[name] = journal;
                journalNames[i] = name;
            }
            for (int i = 0; i < papers.Length; ++i)
            {
                string[] parts = papers[i].Split('.');
                string name = parts[0];
                string[] refs = parts[1].Split(' ');
                bool[] used = new bool[papers.Length];
                for (int j = 0; j < refs.Length; ++j)
                {
                    if (!string.IsNullOrEmpty(refs[j]))
                    {
                        int who = int.Parse(refs[j]);
                        if (!used[who])
                        {
                            used[who] = true;
                            if (journalNames[who] != name)
                            {
                                map[journalNames[who]].iRefs++;
                            }
                        }
                    }
                }
            }
            Journal[] journals = new Journal[map.Count];
            map.Values.CopyTo(journals, 0);
            for (int i = 0; i < journals.Length; ++i)
                for (int j = i + 1; j < journals.Length; ++j)
                {
                    if (more(journals[j], journals[i]))
                    {
                        Journal temp = journals[i];
                        journals[i] = journals[j];
                        journals[j] = temp;
                    }
                }
            string[] result = new string[journals.Length];
            for (int i = 0; i < journals.Length; ++i)
            {
                result[i] = journals[i].name;
            }
            return result;
        }
        static void Main(string[] args)
        {
            AcademicJournal aj = new AcademicJournal();
            string[] result;
            result = aj.rankByImpact(new string[] { "A.", "B. 0", "C. 1 0 3", "C. 2" });
            result = aj.rankByImpact(new string[] { "RESPECTED JOURNAL.", "MEDIOCRE JOURNAL. 0", "LOUSY JOURNAL. 0 1",
                "RESPECTED JOURNAL.", "MEDIOCRE JOURNAL. 3", "LOUSY JOURNAL. 4 3 3 4",
                "RESPECTED SPECIFIC JOURNAL.", "MEDIOCRE SPECIFIC JOURNAL. 6", "LOUSY SPECIFIC JOURNAL. 6 7" });
            result = aj.rankByImpact(new string[] { "NO CITATIONS.", "COMPLETELY ORIGINAL." });
            result = aj.rankByImpact(new string[] { "CONTEMPORARY PHYSICS. 5 4 6 8 7 1 9", "EUROPHYSICS LETTERS. 9",
                "J PHYS CHEM REF D. 5 4 6 8 7 1 9", "J PHYS SOC JAPAN. 5 4 6 8 7 1 9", "PHYSICAL REVIEW LETTERS. 5 6 8 7 1 9",
                "PHYSICS LETTERS B. 6 8 7 1 9", "PHYSICS REPORTS. 8 7 1 9", "PHYSICS TODAY. 1 9",
                "REP PROGRESS PHYSICS. 7 1 9", "REV MODERN PHYSICS." });
            Console.ReadLine();
        }
    }
