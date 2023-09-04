import {AfterContentInit, AfterViewInit, Component, OnInit} from '@angular/core';
import {Note} from "../entity/note";
import {MatDialog} from "@angular/material/dialog";

import {NoteDialogComponent} from "./note-dialog/note-dialog.component";
import {NoteService} from "./note.service";
import {DeleteDialogComponent} from "../delete-dialog/delete-dialog.component";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit, AfterViewInit{

  notes: Note[] = [];
  filteredNotes: Note[] = this.notes;
  title: string = "";
  content: string = "";
  active: string = "true";
  allTags: string[] = [];
  selectedTags: any;
  toppings: any = null;
  spinner: boolean = true;

  constructor(
    private noteService: NoteService,
    public dialog: MatDialog,
    private _formBuilder: FormBuilder) {}

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.getNotes();
    this.noteService.getAllTags().subscribe(data => {
      let jsonObject: any = {};
      this.allTags = data.map(t => t.name);
      this.allTags.forEach(t =>
        jsonObject[t] = false
      );
      this.toppings = this._formBuilder.group(jsonObject);
      this.spinner = false;
    });
  }

  getNotes(){
    this.noteService.getAllNotes().subscribe(data =>{
      this.notes = data;
      this.filterNotes(this.active);
    });
  }

  createEditNote(): void{
    const dialogRef = this.dialog.open(NoteDialogComponent, {
      data: {title: this.title, content: this.content},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      this.noteService.createNote(result).subscribe(_ => this.getNotes());
      // this.title = result;
    });
  }


  // Function to filter notes based on the selected filter
  filterNotes(filter: any): void {
    this.active = filter;
    this.filteredNotes = this.notes.filter(note =>
      filter == 'true' ? !note.isArchived : note.isArchived
    );
  }

  // filterNotes(active: boolean): void {
  //   this.active = filter;
  //   // Determine the tags to filter by based on active state.
  //   const tagsToFilterBy = active ? [] : this.selectedTags;
  //
  //   // Use the filter function to get filtered notes.
  //   this.filteredNotes = this.filterNotesByTags(tagsToFilterBy);
  // }


  // Initialize filteredNotes with all notes


  editNote(note: Note) {
    const dialogRef = this.dialog.open(NoteDialogComponent, {
      data: note,
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      if (result.id)
        this.noteService.updateNote(result.id, result).subscribe(_ => this.getNotes());
      // this.title = result;
    });
  }

  deleteNote(note: Note) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      if(result && note.id) {
        this.noteService.deleteNote(note.id).subscribe(_ => this.getNotes());
      }
    });
  }


  toggleArchivedNote(note: Note) {
    note.isArchived = !note.isArchived;
    if (note.id)
      this.noteService.updateNote(note.id, note).subscribe(_ => this.getNotes())
  }

  filterNotesByTags(tags: string[]): Note[] {
    if (!tags || tags.length === 0) {
      return this.notes; // If no tags are selected, return all notes.
    }

    return this.notes.filter((note) => {
      // Check if the note has all the selected tags.
      return tags.every((tag) => note.tags.some((noteTag) => noteTag.name === tag));
    });
  }

  onCheckbox() {
    const checkedToppings = Object.keys(this.toppings.controls).filter(
      topping => this.toppings.get(topping)?.value === true
    );
    this.filterNotes(this.active);

    if (checkedToppings.length > 0)
      this.filteredNotes = this.filteredNotes.filter(n => n.tags.some((tag) => checkedToppings.includes(tag.name)));
  }
}

