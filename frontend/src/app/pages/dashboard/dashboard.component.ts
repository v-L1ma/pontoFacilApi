import { Component, OnInit } from "@angular/core";
import { Chart,registerables } from "chart.js";
import { MatIcon } from "@angular/material/icon";

Chart.register(...registerables)
@Component({
  selector: 'app-dashboard',
  imports: [
    MatIcon
],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit{

  gerarConfig(tipo:string,data:any){
  const config:any = {
    type: tipo,
    data: data,
    options: {
      scales: {
        y: {
          beginAtZero: true
        }
      }
    },
    };

    return config;
  }
 
  colaboradorDepartamentoChart:any;
  colaboradoresTotalTempo:any;

  ngOnInit(): void {
    this.colaboradorDepartamentoChart = new Chart("colaboradorDepartamentoChart",this.gerarConfig('bar',{
      labels:['JAN','FEB', 'MAR','APRIL'],
      datasets:[
        {
          label:'Sales',
          data:['475','543','312','588'],
          backgroundColor:'blue'
        },
        {
          label:'PAT',
          data:['100','120','133','134'],
          backgroundColor:'red'
        }
      ]
    }));
    this.colaboradoresTotalTempo = new Chart("colaboradoresTotalTempo",this.gerarConfig('line',{
      labels: ['JAN','FEB', 'MAR','APRIL'],
      datasets: [{
        label: 'My First Dataset',
        data: [65, 59, 80, 81, 56, 55, 40],
        fill: false,
        borderColor: 'rgb(75, 192, 192)',
        tension: 0.1
      }]
    }));
  }
}
