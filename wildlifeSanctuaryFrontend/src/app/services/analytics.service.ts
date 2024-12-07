import { Injectable } from '@angular/core';
import Chart from 'chart.js/auto';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {

  constructor() { }
 // General method to create a bar chart
 createBarChart(ctx: HTMLCanvasElement, labels: string[], data: number[], chartTitle: string): void {
console.log("analytical",labels);
console.log("data",data);
  let noDataMessage = '';
  
  if (data.length === 0) {
    data = Array(labels.length).fill(0);  
    noDataMessage = 'No data available'; 
  }

  new Chart(ctx, {
    type: 'bar',
    data: {
      labels:labels,
      datasets: [{
        label: chartTitle,
        data: data,  // Example data
        backgroundColor: '#36A2EB',
        borderColor: '#36A2EB',
        borderWidth: 1
      }]
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: true
        }
      },
      plugins: {
        title: {
          display: true,
          text:chartTitle
        }
      }
    }
  });
 }

 createAreaChart(ctx: HTMLCanvasElement, labels: string[], data: number[], chartTitle: string): void {
  console.log(labels);
  console.log("data",data);
    let noDataMessage = '';
    
    if (data.length === 0) {
      data = Array(labels.length).fill(1);  // Fill with zeros if no data
      noDataMessage = 'No data available';  // Set a no data message
    }
  
    new Chart(ctx, {
      type: 'line',
      data: {
        labels:labels,
        datasets: [{
          label: chartTitle,
          data: data,  // Example data
          backgroundColor: 'rgba(54, 162, 235, 0.2)',
          borderColor: '#36A2EB',
          borderWidth: 1,
          fill: true,
          tension: 0.4
        }]
      },
      options: {
        responsive: true,
        scales: {
          y: {
            beginAtZero: true
          }
        },
        plugins: {
          title: {
            display: true,
            text:chartTitle
          }
        }
      }
    });
   }

createPieChart(ctx: HTMLCanvasElement, labels: string[], data: number[], chartTitle: string): void {
  
  let noDataMessage = '';
  
  if (data.length === 0) {
    data = Array(labels.length).fill(0);  // Fill with zeros if no data
    noDataMessage = 'No data available';  // Set a no data message
  }

  new Chart(ctx, {
    type: 'pie',
    data: {
      labels: labels,
      datasets: [{
        data: data, 
        backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
      }]
    },
    options: {
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: chartTitle
        },
        tooltip: {
          callbacks: {
            label: (tooltipItem) => `${tooltipItem.label}: ${tooltipItem.raw}%`
          }
        }
      }
    }
  });
  
}

createDonutChart(ctx: HTMLCanvasElement, labels: string[], data: number[], chartTitle: string): void {
  let noDataMessage = '';

  if (data.length === 0) {
    data = Array(labels.length).fill(0);  
    noDataMessage = 'No data available';  
  }

  new Chart(ctx, {
    type: 'pie',  
    data: {
      labels: labels,
      datasets: [{
        data: data, 
        backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#FF9F40'],
        borderColor: '#ffffff', // Optional: remove border
        borderWidth: 2
      }]
    },
    options: {
      responsive: true,
      cutout: '70%',  // This creates the donut effect (hole in the middle)
      plugins: {
        title: {
          display: true,
          text: chartTitle
        },
        tooltip: {
          callbacks: {
            label: (tooltipItem) => `${tooltipItem.label}: ${tooltipItem.raw}%`
          }
        }
      }
    }
  
  
});
}
}
 