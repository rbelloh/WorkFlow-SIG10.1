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
  createLineChart: (canvasId, chartData, chartOptions) => {
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
                    // Use color from chartOptions if provided, otherwise default to a suitable color
                    color: chartOptions && chartOptions.plugins && chartOptions.plugins.legend && chartOptions.plugins.legend.labels && chartOptions.plugins.legend.labels.color ? chartOptions.plugins.legend.labels.color : '#e3e3e3' // Default to primary text color
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
            tension: 0 // Straight lines
          }
        },
        ...chartOptions // Merge additional options
      }
    });
  },

  createBarChart: (canvasId, chartData, chartOptions) => {
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
        },
        ...chartOptions // Merge additional options
      }
    });
  },

  createPieChart: (canvasId, chartData, chartOptions) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    const existingChart = Chart.getChart(ctx);
    if (existingChart) {
        existingChart.destroy();
    }

    new Chart(ctx, {
      type: 'pie',
      data: chartData,
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'right',
                labels: {
                    color: '#e3e3e3' // Gemini primary text
                }
            }
        },
        ...chartOptions // Merge additional options
      }
    });
  }
};

window.maximizeWindow = () => {
    try {
        if (window.self !== window.top) { // Check if in an iframe
            // Cannot maximize if in an iframe
            return;
        }
        // For modern browsers, this might not work if the window wasn't opened by script
        // or if user settings prevent it.
        window.moveTo(0, 0);
        window.resizeTo(screen.width, screen.height);
    } catch (e) {
        console.warn("Could not maximize window, browser security restrictions might apply.", e);
    }
};

window.clearApplicationCookies = () => {
    // Clear specific cookies by setting their expiration to a past date
    // This is a best-effort attempt, HttpOnly cookies cannot be cleared by JavaScript
    document.cookie = ".AspNetCore.Identity.Application=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    // Add other application-specific cookies here if needed
    console.log("Attempted to clear application cookies.");
};

// Inactivity Logout
let inactivityTimeout;
let blazorLogoutInvoker; // To hold the .NET method invoker

window.initializeInactivityDetection = (dotNetObject, timeoutMinutes) => {
    blazorLogoutInvoker = dotNetObject;
    const timeoutMilliseconds = timeoutMinutes * 60 * 1000;

    function resetInactivityTimer() {
        clearTimeout(inactivityTimeout);
        inactivityTimeout = setTimeout(logoutDueToInactivity, timeoutMilliseconds);
    }

    function logoutDueToInactivity() {
        if (blazorLogoutInvoker) {
            blazorLogoutInvoker.invokeMethodAsync('LogoutUser');
        }
    }

    // Listen for user activity events
    ['mousemove', 'keydown', 'click', 'scroll'].forEach(event => {
        document.addEventListener(event, resetInactivityTimer);
    });

    resetInactivityTimer(); // Initialize the timer
};

window.clearInactivityDetection = () => {
    clearTimeout(inactivityTimeout);
    ['mousemove', 'keydown', 'click', 'scroll'].forEach(event => {
        document.removeEventListener(event, resetInactivityTimer);
    });
    blazorLogoutInvoker = null;
};