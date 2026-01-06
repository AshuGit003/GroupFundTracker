import { Component, EventEmitter, Output } from '@angular/core';
import { MembersService } from '../../../core/services/members.service';
import { NgbAlert } from "@ng-bootstrap/ng-bootstrap";
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-add-member',
  standalone: true,
  imports: [ FormsModule, NgbAlert, NgIf],
  templateUrl: './add-member.component.html',
  styleUrl: './add-member.component.css'
})
export class AddMemberComponent {

  name = '';
  role = 'Member';

  successMessage = '';
  errorMessage = '';

  @Output() memberAdded = new EventEmitter<void>(); 

  constructor(private membersService: MembersService) {}

  addMember() {
    this.successMessage = '';
    this.errorMessage = '';

    if(!this.name.trim()) {
      this.errorMessage = 'Member name is required';
      return;
    }

    this.membersService.addMember({
      name: this.name,
      role: this.role
    }).subscribe({
      next: () => {
        this.successMessage = 'Member added successfully!';
        this.name = '';
        this.role = 'Member';
        this.memberAdded.emit();
      },
      error: () => {
        this.errorMessage = 'Failed to add member';
      }
    })
  }
}
