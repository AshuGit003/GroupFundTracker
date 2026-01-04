import { Component, OnInit } from '@angular/core';
import { MembersService, Member } from '../../../core/services/members.service';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-members-list',
  imports: [CommonModule, NgbModule],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {
  members: Member[] = [];
  constructor(private memberservice: MembersService){}

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberservice.getMembers().subscribe(data=>{
      this.members = data;
    })
  }
}
