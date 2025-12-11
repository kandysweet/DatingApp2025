import { Component, inject, OnInit, signal } from '@angular/core';

import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

import { Member } from '../../../types/member';
import { filter } from 'rxjs';

@Component({
  selector: 'app-member-detail',
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './member-detail.html',
  styleUrl: './member-detail.css'
})
export class MemberDetail implements OnInit {

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  protected member = signal<Member | undefined>(undefined);
