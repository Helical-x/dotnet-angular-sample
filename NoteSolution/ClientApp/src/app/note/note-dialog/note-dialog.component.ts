import {Component, ElementRef, Inject, inject, OnInit, ViewChild} from '@angular/core';
import {Note} from "../../entity/note";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Tag} from "../../entity/tag";
import {FormControl} from "@angular/forms";
import {LiveAnnouncer} from "@angular/cdk/a11y";
import {MatChipInputEvent} from "@angular/material/chips";
import {NoteService} from "../note.service";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {Observable, startWith} from "rxjs";
import {MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {map} from "rxjs/operators";

export interface DialogData {
  title: string;
  content: string;
}
@Component({
  selector: 'app-dialog',
  templateUrl: './note-dialog.component.html',
  styleUrls: ['./note-dialog.component.css']
})
export class NoteDialogComponent implements OnInit{


  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagCtrl = new FormControl('');
  filteredTags: Observable<string[]>;
  tags: string[] = [];
  allTags: string[] = [];
  rawTags: Tag[] = [];

  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement> | undefined;

  announcer = inject(LiveAnnouncer);
  constructor(
    private noteService: NoteService,
    public dialogRef: MatDialogRef<NoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Note,
  ) {
    this.filteredTags = this.tagCtrl.valueChanges.pipe(
      startWith(null),
      map((tag: string | null) => (tag ? this._filter(tag) : this.allTags.slice())),
    );
  }

  ngOnInit(): void{
    this.tags = this.data.tags.map(t => t.name);
    this.noteService.getAllTags().subscribe(data => {
      this.allTags = data.map(t => t.name);
      this.rawTags = data;
      this.filteredTags = this.tagCtrl.valueChanges.pipe(
        startWith(null),
        map((tag: string | null) => (tag ? this._filter(tag) : this.allTags.slice())),
      );
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our tag
    if (value) {
      this.tags.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();

    this.tagCtrl.setValue(null);
  }

  remove(tag: string): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);

      this.announcer.announce(`Removed ${tag}`);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.tags.push(event.option.viewValue);
    // @ts-ignore
    this.tagInput.nativeElement.value = '';
    this.tagCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allTags.filter(tag => tag.toLowerCase().includes(filterValue));
  }


  save() {
    this.data.tags = this.tags.map(tagName => {
      const tag = this.rawTags.find(rawTag => rawTag.name === tagName);
      return tag ? { id: tag.id, name: tagName } : { name: tagName };
    });
    this.dialogRef.close(this.data);
  }

}
