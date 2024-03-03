$(document).ready(function () {
    //getCarNo();
    load();
    available();
    //function getCarNo() {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/Rentify/GetCarNo',
    //        dataType: 'JSON',
    //        success: function (data) {
    //            console.log("data received:", data);
    //            for (var i = 0; i < data.length; i++) {
    //                $("#CarNo").append($("<option/>", {
    //                    text: data[i].CarNo,
    //                    value: data[i].CarNo
    //                }));
    //            }
    //        }
    //    });
    //}

    //$("#CarNo").change(function () {
    //    var selectedCarNo = $(this).val();
    //    getFees(selectedCarNo);
    //    available();
    //});

    //function getFees(carNo) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/Rentify/GetFees?carNo=' + carNo,
    //        dataType: 'JSON',
    //        success: function (fees) {
    //            console.log("fees received:", fees);
    //            $("#fees").val(fees);
    //            calculateTotalAmount();
    //        }
    //    });
    //}

    function load() {
        $("#fees, #StartDate, #EndDate, #TotalAmount").attr("disabled", "disabled");
    }

    function available() {
        $.ajax({
            type: 'POST',
            url: '/Rentify/GetAvil?CarNo=' + $("#CarNo").val(),
            dataType: 'JSON',
            success: function (data) {
                console.log(data);
                var avail = data;
                if (avail === "Yes") {
                    enableInputs();
                } else {
                    disableInputs();
                }
            }
        });
    }

    function enableInputs() {
        $("#fees, #StartDate, #EndDate, #TotalAmount").removeAttr('disabled');
    }

    function disableInputs() {
        $("#fees, #StartDate, #EndDate, #TotalAmount").attr("disabled", "disabled");
    }

    function calculateTotalAmount() {
        var fees = parseFloat($("#fees").val());
        var startDate = new Date($("#StartDate").val());
        var endDate = new Date($("#EndDate").val());
        var daysDifference = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24));
        var totalAmount = fees * daysDifference;
        $("#TotalAmount").val(totalAmount.toFixed(0));
    }

    $("#StartDate, #EndDate").change(function () {
        calculateTotalAmount();
    });

    $('#container1 form').submit(function (event) {
        event.preventDefault();
        var formData = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: '/Rentify/SaveData',
            data: formData,
            success: function (response) {
                alert("Data successfully saved.");               
            },
            error: function (xhr, status, error) {
                alert("Error occurred while saving data.");
            }
        });
    });

  
    document.addEventListener("DOMContentLoaded", function () {
        var startDateInput = document.getElementById("StartDate");
        var endDateInput = document.getElementById("EndDate");

       
        var today = new Date().toISOString().split("T")[0];
        startDateInput.min = today;

       
        endDateInput.style.display = "none";

        
        startDateInput.addEventListener("change", function () {
            
            var selectedDate = new Date(startDateInput.value);
            if (selectedDate < new Date(today)) {
                alert("Please select today or a future date for the start date.");
                startDateInput.value = ""; 
                return;
            }

            
            endDateInput.style.display = "block";
            endDateInput.min = startDateInput.value;
        });

      
        endDateInput.addEventListener("change", function () {
            
            if (endDateInput.value < startDateInput.value) {
                endDateInput.value = ""; // Clear the end date input
                alert("End date cannot be before start date");
            }
        });

      
        document.getElementById("myForm").addEventListener("submit", function (event) {
           
            if (endDateInput.value < startDateInput.value) {
                event.preventDefault();
                alert("End date cannot be before start date");
            }
        });
    });
   
    $(document).ready(function () {
        $('#rental-form').submit(function (event) {
            event.preventDefault();
            var formData = $(this).serialize();
            $.ajax({
                type: 'POST',
                url: '/Rentify/SaveData',
                data: formData,
                success: function (response) {
                    alert("Car Booked successfully.");
                    reload()
                },
                error: function (xhr, status, error) {
                    alert("Error occurred while saving data.");
                }
            });
        });
    });

});
