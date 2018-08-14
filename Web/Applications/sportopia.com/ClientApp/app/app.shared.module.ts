import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { Master } from './Master';
import * as Pages from './Pages/_include';
import * as Controls from './Controls/_include';
import * as System from './System/_include';

@NgModule({
	declarations: [
		Master,
		Pages.Home,
		Controls.Navigation.Left
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: Pages.Home },
            { path: '**', redirectTo: 'home' }
        ])
	],
	providers: [ System.STA.Context, System.LMS.Context ]
})
export class AppModuleShared {
}
