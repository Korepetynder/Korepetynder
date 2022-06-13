import { MultimediaFile } from "src/app/settings/models/responses/multimediaFile";
import { Location } from "../../settings/models/responses/location";
import { TutorLesson } from "../../settings/models/responses/tutorLesson";

export interface TutorDetails {
  id: string;
  fullName: string;
  email: string;
  phoneNumber: string | null;
  locations: Location[];
  lessons: TutorLesson[];
  score: number;
  multimediaFiles: MultimediaFile[];
}
