import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DisponibilitesPage } from './disponibilites';

@NgModule({
  declarations: [
    DisponibilitesPage,
  ],
  imports: [
    IonicPageModule.forChild(DisponibilitesPage),
  ],
})
export class DisponibilitesPageModule {}
