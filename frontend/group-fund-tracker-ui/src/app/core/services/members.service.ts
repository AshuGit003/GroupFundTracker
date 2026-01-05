import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment'
import { HttpClient } from '@angular/common/http';

export interface Member {
  memberId: number;
  name: string;
  role: string;
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private baseUrl = `${environment.apiBaseUrl}/members`;

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl);
  }

  addMember(member: Partial<Member>) {debugger
    return this.http.post<Member>(this.baseUrl, member);
  }
}
