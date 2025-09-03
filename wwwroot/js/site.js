window.scrollToTop = () => {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
};

window.addEventListener('scroll', () => {
    const scrollToTopBtn = document.getElementById('scrollToTopBtn');
    if (scrollToTopBtn) {
        if (window.pageYOffset > 200) { // Show button after scrolling 200px
            scrollToTopBtn.classList.add('show');
        } else {
            scrollToTopBtn.classList.remove('show');
        }
    }
});

window.blazorCharts = {
  createLineChart: (canvasId, chartData) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    const existingChart = Chart.getChart(ctx);
    if (existingChart) {
        existingChart.destroy();
    }

    new Chart(ctx, {
      type: 'line',
      data: chartData,
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
                labels: {
                    color: '#e3e3e3' // Gemini primary text
                }
            },
        },
        scales: {
          y: {
            beginAtZero: true,
            max: 100,
            ticks: {
              callback: function(value) {
                return value + '%';
              },
              color: '#9aa0a6' // Gemini secondary text
            },
            grid: {
                color: '#3c4043' // Gemini border color
            }
          },
          x: {
            ticks: {
              color: '#9aa0a6' // Gemini secondary text
            },
            grid: {
                color: '#3c4043' // Gemini border color
            }
          }
        },
        elements: {
          line: {
            tension: 0.4 // This creates the curved lines
          }
        }
      }
    });
  },

  createBarChart: (canvasId, chartData) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    const existingChart = Chart.getChart(ctx);
    if (existingChart) {
        existingChart.destroy();
    }

    new Chart(ctx, {
      type: 'bar',
      data: chartData,
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: false
            }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              callback: function(value) {
                return value + '%';
              },
              color: '#9aa0a6' // Gemini secondary text
            },
            grid: {
                color: '#3c4043' // Gemini border color
            }
          },
          x: {
             grid: {
                display: false
             },
             ticks: {
                color: '#9aa0a6' // Gemini secondary text
             }
          }
        }
      }
    });
  }
};