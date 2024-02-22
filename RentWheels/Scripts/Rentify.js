$(document).ready(function () {
    // Get Car Number in Selection Bar
    getCarNo();

    // Function to get car numbers and append options to the select element
    function getCarNo() {
        $.ajax({
            type: 'GET',
            url: '/Rentify/GetCarNo',
            dataType: 'JSON',
            success: function (data) {
                console.log("data received:", data);
                for (var i = 0; i < data.length; i++) {
                    $("#CarNo").append($("<option/>", {
                        text: data[i].CarNo,
                        value: data[i].CarNo
                    }));
                }
            }
        });
    }

    $("#CarNo").change(function () {
        var selectedCarNo = $(this).val();
        getFees(selectedCarNo);
    });

    function getFees(carNo) {
        $.ajax({
            type: 'GET',
            url: '/Rentify/GetFees?carNo=' + carNo,
            dataType: 'JSON',
            success: function (fees) {
                console.log("fees received:", fees);
                $("#fees").val(fees);
                calculateTotalAmount();
            }
        });
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
                window.location.href = '/Rentify/Rentify';
            },
            error: function (xhr, status, error) {
                alert("Error occurred while saving data.");
            }
        });
    });
});
