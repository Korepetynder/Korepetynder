export class LocationRequest {
  name: String;
  parentLocationId: number;

  constructor(name: String, parentLocationId: number) {
    this.name = name;
    this.parentLocationId = parentLocationId;
  }
}
