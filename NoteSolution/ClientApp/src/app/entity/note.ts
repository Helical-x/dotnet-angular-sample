import {Tag} from "./tag";

export interface Note {
  id?: number,
  title?: string,
  content?: string,
  isArchived?: boolean,
  tags: Tag[]
}
