import { TutorDetails } from './tutorDetails';
import { Subject } from "../../settings/models/responses/subject";
import { Level } from "../../settings/models/responses/level";
import { Language } from "../../settings/models/responses/language";

export const MockTutors: TutorDetails[] = [
  {
    fullName: "Mr Nice",
    email: "mrnice@gmial.com",
    phoneNumber: "+48 345 457 569",
    locations: [
      { id: 1, name: "NYC", parentId: null, childrenLocations: [] }
    ],
    lessons: [
      {
        id: 1, cost: 100, frequency: 2,
        subject: { id: 1, name: "matematyka" },
        levels: [{ id: 1, name: "studia" }, { id: 2, name: "liceum" }, { id: 3, name: "szkoła podstawowa" }],
        languages: [{ id: 1, name: "polski" }, { id: 2, name: "angielski" }]
      },
      {
        id: 2, cost: 100, frequency: 2,
        subject: { id: 1, name: "matematyka" },
        levels: [{ id: 1, name: "studia" }, { id: 2, name: "liceum" }, { id: 3, name: "szkoła podstawowa" }],
        languages: [{ id: 1, name: "polski" }, { id: 2, name: "angielski" }]
      }
    ],
    score: 3.5,
    isFavorite: false
  },
  {
    fullName: "Mrs Marvel",
    email: "marvel@gmial.com",
    phoneNumber: "+48 111 457 111",
    locations: [
      { id: 1, name: "NYC", parentId: null, childrenLocations: [] }
    ],
    lessons: [
      {
        id: 1, cost: 100, frequency: 2,
        subject: { id: 1, name: "matematyka" },
        levels: [{ id: 1, name: "studia" }],
        languages: [{ id: 1, name: "polski" }]
      }
    ],
    isFavorite: false,
    score: 3.5,
  },
  {
    fullName: "Mr Magneto",
    email: "magneto@gmial.com",
    phoneNumber: "+48 345 457 569",
    locations: [
      { id: 1, name: "NYC", parentId: null, childrenLocations: [] }
    ],
    lessons: [
      {
        id: 1, cost: 100, frequency: 2,
        subject: { id: 1, name: "matematyka" },
        levels: [{ id: 1, name: "studia" }],
        languages: [{ id: 1, name: "polski" }]
      }
    ],
    isFavorite: false,
    score: 3.5,
  }
];
