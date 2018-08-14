import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.shared.module';
import { Master } from './Master';

@NgModule({
    bootstrap: [ Master ],
    imports: [
        ServerModule,
        AppModuleShared
    ]
})
export class AppModule {
}
