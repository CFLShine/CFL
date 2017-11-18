import { NgModule } from '@angular/core';
import { ZoneFilterPipe } from './zone-filter/zone-filter';
import { AstreinteFilterDayPipe } from './astreinte-filter-day/astreinte-filter-day';
import { OrderByIdPipe } from './order-by-id/order-by-id';
@NgModule({
	declarations: [ZoneFilterPipe,
    AstreinteFilterDayPipe,
    OrderByIdPipe],
	imports: [],
	exports: [ZoneFilterPipe,
    AstreinteFilterDayPipe,
    OrderByIdPipe]
})
export class PipesModule {}
