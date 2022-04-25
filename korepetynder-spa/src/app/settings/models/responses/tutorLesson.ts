import { Language } from "./language";
import { Level } from "./level";
import { Subject } from "./subject";

export interface TutorLesson {
  id: number;
  cost: number;
  frequency: number | null;
  subject: Subject;
  levels: Level[];
  languages: Language[];
}
