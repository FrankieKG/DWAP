
     document.getElementById("excel-file").addEventListener("change", handleFileSelect);

function handleFileSelect(evt) {
    var file = evt.target.files[0];
    if (!file) {
        console.log("No file selected");
        return;
    }
    console.log("File selected");

    var formData = new FormData();
    formData.append('file', file);

    fetch('/Home/Upload', {
        method: 'POST',
        body: formData,
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log("File uploaded successfully");
            console.log(data);
        })
        .catch(error => {
            console.error("Error:", error);
        });
}


    function createColumnCheckboxes(columns) {
        const columnCheckboxes = document.getElementById("column-checkboxes");
        columnCheckboxes.innerHTML = "";
        columns.forEach((column) => {
            const label = document.createElement("label");
            label.textContent = column;

            const checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.name = "column";
            checkbox.value = column;

            label.appendChild(checkbox);
            columnCheckboxes.appendChild(label);
        });

        document.getElementById("column-selection").classList.remove("hidden");
    }

    function createChart(data) {
        const ctx = document.getElementById("chart").getContext("2d");
        const selectedColumns = getSelectedColumns();

        if (selectedColumns.length >= 2) {
            const labels = data.map((row) => row[selectedColumns[0]]);
            const datasetData = data.map((row) => row[selectedColumns[1]]);

            const chart = new Chart(ctx, {
                type: "line",
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: selectedColumns[1],
                            data: datasetData,
                            borderColor: "rgba(75, 192, 192, 1)",
                            borderWidth: 1,
                        },
                    ],
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true,
                        },
                    },
                },
            });

            document.getElementById("chart-container").classList.remove("hidden");
        }
    }

    function getSelectedColumns() {
        const selectedColumns = [];
        document.querySelectorAll("input[name=column]:checked").forEach((checkbox) => {
            selectedColumns.push(checkbox.value);
        });
        return selectedColumns;
    }

    document.querySelectorAll("input[name=column]").forEach((checkbox) => {
        checkbox.addEventListener("change", () => {
            const selectedColumns = getSelectedColumns();
            if (selectedColumns.length >= 2) {
                createChart();
            } else {
                document.getElementById("chart-container").classList.add("hidden");
            }
        });
    });



