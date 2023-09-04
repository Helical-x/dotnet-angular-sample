import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Note} from "../entity/note";
import {Tag} from "../entity/tag";

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAllNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.baseUrl + 'note');
  }

  getNoteById(id: number): Observable<Note> {
    return this.http.get<Note>(`${this.baseUrl}/${id}`);
  }

  createNote(note: Note): Observable<Note> {
    return this.http.post<Note>(this.baseUrl + 'note', note);
  }

  updateNote(id: number, note: Note): Observable<Note> {
    return this.http.put<Note>(`${this.baseUrl}note/${id}`, note);
  }

  deleteNote(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}note/${id}`);
  }

  getAllTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(this.baseUrl + 'tag');
  }
}
