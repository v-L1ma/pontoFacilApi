import { Component, OnInit } from "@angular/core";
import { Chart,registerables } from "chart.js";
import { MatIcon } from "@angular/material/icon";
import { ColaboradoresService } from "../../shared/services/colaboradores/colaboradores.service"; 
import { ColaboradoresStats } from "../../shared/types/types";

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
  data:ColaboradoresStats={
    novosColaboradoresMes:0,
    colaboradoresAtivos:0,
    colaboradoresDemitidos:0,
    colaboradorDepartamento:{
      labels:["Nenhum"],
      dataset:{
        data:[0],
        label:"Departamentos"
      }
    },
    colaboradoresTotalTempo:{
      labels:["10/2025"],
      dataset:{
        data:[0],
        label:"Admitidos"
      }
    },
    colaboradoresDemitidoMes:{
      labels:["10/2025"],
      dataset:{
        data:[0],
        label:"Demitidos"
      }
    },
  };

  constructor(
    private colaboradoresService:ColaboradoresService
  ){}

  ngOnInit(): void {

    this.colaboradoresService.estatisticas().subscribe({
      next:(response)=>{
        console.log(response)
        this.data = response.dados
        this.colaboradorDepartamentoChart = new Chart("colaboradorDepartamentoChart",this.gerarConfig('bar',{
          labels:this.data.colaboradorDepartamento.labels,
          datasets:[
            {
              label:this.data.colaboradorDepartamento.dataset.label,
              data:this.data.colaboradorDepartamento.dataset.data,
              backgroundColor:'rgb(75, 192, 192)'
            }
          ]
        }));
        this.colaboradoresTotalTempo = new Chart("colaboradoresTotalTempo",this.gerarConfig('line',{
          labels:this.data.colaboradoresTotalTempo.labels,
          datasets: [{
            label: this.data.colaboradoresTotalTempo.dataset.label,
            data: this.data.colaboradoresTotalTempo.dataset.data,
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            tension: 0.1
          },{
            label: this.data.colaboradoresDemitidoMes.dataset.label,
            data: this.data.colaboradoresDemitidoMes.dataset.data,
            fill: false,
            borderColor: 'rgba(201, 57, 57, 1)',
            tension: 0.1
          }]
        }));
      },
      error:(erro)=>{
        console.log(erro)
      }
    })

    
  }
}
