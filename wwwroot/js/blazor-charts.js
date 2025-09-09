window.blazorCharts = {
    _charts: {}, // Private registry to store chart instances

    createChart: function (canvasId, type, data, options) {
        var ctx = document.getElementById(canvasId).getContext('2d');

        // Destroy existing chart if it exists
        if (window.blazorCharts._charts[canvasId]) {
            window.blazorCharts._charts[canvasId].destroy();
        }

        // Create new chart and store its instance
        window.blazorCharts._charts[canvasId] = new Chart(ctx, {
            type: type,
            data: data,
            options: options
        });
    },

    createBarChart: function (canvasId, data, options) {
        this.createChart(canvasId, 'bar', data, options);
    },
    createPieChart: function (canvasId, data, options) {
        this.createChart(canvasId, 'pie', data, options);
    },
    createLineChart: function (canvasId, data, options) {
        this.createChart(canvasId, 'line', data, options);
    }
};