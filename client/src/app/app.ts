import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { User } from '../types/user';
import { Router, RouterOutlet } from "@angular/router";
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [Nav, RouterOutlet, NgClass],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected router = inject(Router);
  protected readonly title = signal("Dating App");

  // Se cargan los elementos cada vez que se refresca la página
  protected members = signal<User[]>([]);

  // Otra forma de llamarlo desde el html
  // quedaria de la siguiente forma:
  // <h1>{{title2}}</h1>
  // protected title2 = "Hola";

  // Constructor tradicional que se puede susituir
  // con el de inject
  // constructor(private http2: HttpClient) { }

  async ngOnInit(): Promise<void> {
    this.setCurrentUser();
    this.members.set(await this.getMembers());
  }

  setCurrentUser() {
    const userString = localStorage.getItem("user");
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers(): Promise<User[]> {
    try {
      return await lastValueFrom(this.http.get<User[]>("https://localhost:5001/api/members"))
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

}

