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
            },
        },
        scales: {
          y: {
            beginAtZero: true,
            max: 100,
            ticks: {
              callback: function(value) {
                return value + '%';
              }
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
                display: false // Legend is often not needed for this type of bar chart
            }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              callback: function(value) {
                return value + '%'; // Add percentage sign to Y-axis
              }
            }
          },
          x: {
             grid: {
                display: false // Hide vertical grid lines for a cleaner look
             }
          }
        }
      }
    });
  }
};
