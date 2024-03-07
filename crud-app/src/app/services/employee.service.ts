import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  api = 'https://localhost:7046/api/Employees'
  constructor(private _http: HttpClient) {}

  addEmployee(data: any): Observable<any> {
    return this._http.post(this.api, data);
  }

  updateEmployee(id: number, data: any): Observable<any> {
    return this._http.put(this.api+id, data);
  }

  getEmployeeList(pageNumber?: number, pageSize?: number): Observable<any> {
    return this._http.get(`${this.api}?pageNumber=${pageNumber??1}&pageSize=${pageSize??10}`);
  }

  deleteEmployee(id: number): Observable<any> {
    return this._http.delete(this.api+id);
  }
}
