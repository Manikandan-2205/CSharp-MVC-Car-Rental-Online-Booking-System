$(document).ready(function () {
    // Get Car Number in Selection Bar
    getCarNo();

    load();

    // Function to get car numbers and append options to the select element
    function getCarNo() {
        $.ajax({
            type: 'GET',
            url: '/CarBack/GetCarNo',
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
            url: '/CarBack/GetFees?carNo=' + carNo,
            dataType: 'JSON',
            success: function (fees) {
                console.log("fees received:", fees);
                $("#Fees").val(fees);
                //calculateTotalAmount();
            }
        });
    }

    // Disable input fields
    function load() {
        $("#Fees, #ReturnDate, #ExpiredDays, #TotalAmount").attr("disabled", "disabled");
    }

    // Handle CarId selection change
    $("#CarNo").change(function () {
        available();
    });

    // Check if a car is available
    function available() {
        $.ajax({
            type: 'POST',
            url: '/CarBack/GetAvil?CarNo=' + $("#CarNo").val(),
            dataType: 'JSON',
            success: function (data) {
                console.log(data);

                var avail = data;

                if (avail === "No") {
                    enableInputs();
                } else {
                    disableInputs();
                }
            }
        });
    }

    // Enable input fields
    function enableInputs() {
        $("#Fees, #ReturnDate, #ExpiredDays, #TotalAmount").removeAttr('disabled');
    }

    // Disable input fields
    function disableInputs() {
        $("#Fees, #ReturnDate, #ExpiredDays, #TotalAmount").attr("disabled", "disabled");
    }

    $("#ReturnDate").on("change", function () {
        var carno = $("#CarNo").val();
        getLatestBookingEndDate(carno);
    });

    function getLatestBookingEndDate(carNo) {
        $.ajax({
            type: 'GET',
            url: '/CarBack/GetLatestBookingEndDate',
            data: { carNo: carNo },
            dataType: 'json',
            success: function (latestEndDate) {
                try {
                    var endDate = new Date(parseInt(latestEndDate.match(/\d+/)[0]));
                    var returnDate = new Date($("#ReturnDate").val());

                    if (returnDate < new Date()) {
                        throw "Return date is in the past";
                    }

                    var expiredDays = Math.max(0, Math.ceil((returnDate - endDate) / (1000 * 60 * 60 * 24)));

                    $("#ExpiredDays").val(expiredDays);
                    calculateTotalAmount();
                } catch (error) {
                    console.error("Error occurred while calculating expired days:", error);
                    $("#ExpiredDays").val("");
                    $("#TotalAmount").val("");
                    alert(error);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error occurred while getting latest booking end date:", error);
            }
        });
    }

    function calculateTotalAmount() {
        var fees = parseInt($("#Fees").val());
        var expiredDays = parseInt($("#ExpiredDays").val());
        if (isNaN(fees) || isNaN(expiredDays)) {
            $("#TotalAmount").val("");
        } else {
            var totalAmount = fees * expiredDays;
            $("#TotalAmount").val(totalAmount);
        }
    }





    //$('#container1 form').submit(function (event) {
    //    event.preventDefault();
    //    var formData = $(this).serialize();
    //    $.ajax({
    //        type: 'POST',
    //        url: '/CarBack/SaveReturnData',
    //        data: formData,
    //        success: function (response) {
    //            alert("Data successfully saved.");
    //            //window.location.href = '/Rentify/Rentify';
    //        },
    //        error: function (xhr, status, error) {
    //            alert("Error occurred while saving data.");
    //        }
    //    });
    //});
});

