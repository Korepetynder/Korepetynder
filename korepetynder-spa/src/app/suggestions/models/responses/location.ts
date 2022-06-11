export interface Location {
  id: number;
  name: string;
  parentId: number | null;
  childrenLocations: Location[];
}
