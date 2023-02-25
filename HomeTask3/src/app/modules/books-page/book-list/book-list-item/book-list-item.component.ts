import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { IBookData } from 'src/app/modules/shared/interfaces/book-data.interface';
import { ViewBookComponent } from '../../view-book/view-book.component';

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.scss'],
})
export class BookListItemComponent {
  book: IBookData;

  constructor(private dialog: MatDialog) {
    this.book = {
      title: 'The Lord of the Rings',
      genre: 'Fantasy',
      author: 'J. R. R. Tolkien',
      content: 'Long time ago in a galaxy far, far away...',
      cover: `data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAMCAgMCAgMDAwMEAw
        MEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGB
        IUFRT/2wBDAQMEBAUEBQkFBQkUDQsNFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUF
        BQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBT/wAARCAAuAC8DASIAAhEBAxEB/8QAHw
        AAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9
        AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3
        ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKj
        pKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHw
        EAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3
        AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygp
        KjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZa
        XmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3
        +Pn6/9oADAMBAAIRAxEAPwD9U6KZM5jhd1QyMqkhV6n2r4S8f/t7+J9Y8238KaVbeH4Dwt1c4u
        bj6gEBF+hDfWuTEYqnhknUe59Hk3D+Oz6coYOKtG123ZK97efR7Jn3NqWqWej2rXV/dwWNsp
        Aaa5kWNBnpliQKsqwdQykMpGQR0Nfl54N0Hxh+1D4/TRdS168vp3t5p5Lq8laSO2RUOCF6KC5
        RflHV64bwr8XfiL8FNWudO0jxDqGjy2M8kM+myOJbdZFJVwYX3JnIIzjPHWuCOZc3vOHun2WI4
        FdGX1eGKi6ySbVnZJ3trv0fT5d/19pGUMuGGRXxt+zx+3D4h+I3jbRfB+veG7O5vdQk8pNR0+Vo
        dgVCzO8TbtxwpPyso9vT7Kr1KVaFaPNA/P8AMcsxOV1VRxKs2rqzvdf13CvyD8baX/YnjPX9Oxt
        +x6hcW+302SMv9K/XyvzV+JXw3u/F37VuueFbJSs2p60zlgP9WkmJXkPsqszfhXj5tBzjC297fefpv
        hvi4Yevi1Udo8ik/SL1f4ne/CPVT+zd+ztqnxIktopvEPiG6jttMt7gY3QIxJz3AYLK3HB2x+tefftpeDrCf
        xDoXxM8PgP4e8ZWiXJdBwlyEXcDjoWUqSOu5ZPQ17d8cP2mPDnww8QHwNa+AtK8UaZ4eto
        7eM3zqViYRjKKpjYDC7VJ9QfSsfw78RtK/bF+Fni74e23hex8K6vpdsup6JZ2cw8p5FZs7RsUL8z
        BTx0mJrPlpyi8PGV2tlbqt/vOyVbG08RHO69BxjUbcpc0f4crKC5b3XKlF/eeNfsI6Q+pftGaPcIpK6f
        aXdy59AYmiz+co/Ov09r8+/8AgnHok3/Cz/Fl+8bILPSfskgYYKtJOjYPof3J/Kv0Er0Mvjajfuz4vjOr7
        TNOX+WKX5v9Qrxf4xfsw6J8TtRn1/Tr668M+LXQKdTs3bbNhdoEiZHYAZUg4HOcYr2iiu2pThVjy
        zV0fKYLH4nLqyr4WbjL812a2a8noflR8aPgj40+FNxdv4jspLi2lLFdXhYywTk9y/UMfR8H2re/Z9/ZR
        +I/jy8ttctrm68DaOw+XV5GeK4lQgZ8mNSGYEHqSqkZwT0r9NLq1gvoGhuIY7iFsbo5VDKcHIyD
        7gVLXl08spwm3d2/rqfoGL48xmKw0aXsoqpazl0t5RfX715Hn3wf+B/hv4K6XeW+hpcz3uoMsmoal
        fTGWe8dSxDOegxvbhQOvOTk16DRRXrxioLlirI/N61apiKjq1pOUnu2f//Z`,
      rating: 4.5,
      reviews: [
        { message: 'Great book!', reviewer: 'John Doe' },
        { message: 'I liked it.', reviewer: 'Jane Doe' },
        { message: 'It was ok.', reviewer: 'Joe Doe' },
        { message: 'Could be better.', reviewer: 'Jack Doe' },
        { message: 'I did not like it.', reviewer: 'Jill Doe' },
      ],
    };
  }

  openDialogWithBookInfo() {
    this.dialog.open(ViewBookComponent, {
      data: {
        ...this.book,
      },
      width: '700px',
    });
  }
}
