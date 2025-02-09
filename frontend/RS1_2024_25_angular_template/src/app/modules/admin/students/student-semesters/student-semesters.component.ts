import {Component, OnInit} from '@angular/core';
import {
  StudentGetByIdEndpointService,
  StudentGetByIdResponse
} from '../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import {YOSGetResponse} from '../../../../DTOs/YOSGetResponse';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {MatTableDataSource} from '@angular/material/table';
import {MyConfig} from '../../../../my-config';

@Component({
  selector: 'app-student-semesters',
  standalone: false,

  templateUrl: './student-semesters.component.html',
  styleUrl: './student-semesters.component.css'
})
export class StudentSemestersComponent implements OnInit {
  dataSource: MatTableDataSource<YOSGetResponse> = new MatTableDataSource<YOSGetResponse>();
  student: StudentGetByIdResponse | null = null;
  yos: YOSGetResponse[] = [];
  displayedColumns: string[] = ['id', 'academicYear', 'year', 'renewal', "date", "recordedBy"];

  constructor(
    private studentGetService: StudentGetByIdEndpointService,
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void{
      this.loadData();
  }


    loadData(): void {
      this.route.params.subscribe(params => {
        let id = params['id'];
        if (id) {
          this.studentGetService.handleAsync(id).subscribe(studentGet => {
            this.student = studentGet;
          })
          this.http.get<YOSGetResponse[]>(`${MyConfig.api_address}/yos/get/${id}`).subscribe(yos => {
            this.yos = yos;
            this.dataSource.data = this.yos;
          })
        }
      })
    }

  goToNewSemester()
  {
    this.router.navigate(['admin/student/semesters/new', this.student!.id])
  }

}

