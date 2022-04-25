import { DateTime } from 'luxon';

export class UserRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  birthDate: string;

  constructor(firstName: string, lastName: string, email: string, phoneNumber: string, birthDate: DateTime) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.phoneNumber = phoneNumber;
    this.birthDate = birthDate.toISO();
  }
}
