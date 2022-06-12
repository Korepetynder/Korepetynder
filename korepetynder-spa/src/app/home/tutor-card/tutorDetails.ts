import { Location } from "../../settings/models/responses/location";
import { TutorLesson } from "../../settings/models/responses/tutorLesson";

export interface TutorDetails {
  // id: number;
  fullName: string;
  email: string;
  phoneNumber: string | null;
  locations: Location[];
  lessons: TutorLesson[];

  isFavorite: boolean;
  
  starRatingAverage: number;
}
