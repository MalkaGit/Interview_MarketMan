//event handlers

function on_document_ready()
{
    getCelebs_and_render_grid();
}

function on_delete_celeb(index)
{
    showSpinner();

    $.ajax(
        {
            type:       "delete",
            url:        "http://localhost:50631/api/Celebs/" + index,
            //dataType: "json",                                         do not add data type when resposne body is empty. otherwise you get error:     SyntaxError: Unexpected end of JSON input
            //contentType:    "application/json; charset=utf-8",        not needed. no request body.
            success:    function ()
                        {
                            $('#'+index).remove();  //remove row from grid
                            hideSpinner();
                        },

            error:      function (XMLHttpRequest, textStatus, errorThrown)
                        {
                            hideSpinner();
                alert("failed deleting data. Please contat administrator. textStatus=" + textStatus + " errorThrown=" + errorThrown);
                        }
        }
    );

}

function onResetData()
{
    showSpinner();

    $.ajax(
        {
            type:           "post",
            url:            "http://localhost:50631/api/Celebs",
            //dataType: "json",     do not add data type when resposne body is empty. otherwise you get error:     SyntaxError: Unexpected end of JSON input
            //contentType:    "application/json; charset=utf-8",        not needed. no request body.
            success: function ()
            {
                hideSpinner();
                getCelebs_and_render_grid();
            },

            error: function (XMLHttpRequest, textStatus, errorThrown)
            {
                hideSpinner();
                alert("failed to reset data. Please contat administrator. textStatus=" + textStatus + " errorThrown=" + errorThrown);
            }
        }
    );
}

///helpers

function getCelebs_and_render_grid()
{
    showSpinner();

    $.ajax(
        {
            type:       "get",
            dataType:   "json",
            url: "http://localhost:50631/api/Celebs",
            dataType:   "json",                                         //needed to parse resposne body
            //contentType:    "application/json; charset=utf-8",        not needed. no request body.
            success:    function (celebs_array)
                        {
                            var html_table = map_array_to_html_table(celebs_array);
                            $("#table_id").html(html_table);

                            hideSpinner();
                        },

            error:      function (XMLHttpRequest, textStatus, errorThrown)
                        {
                            hideSpinner();
                alert("failed reading data. Please contact administrator. textStatus=" + textStatus + " errorThrown=" + errorThrown);
                        }
        }
    );

}


function map_array_to_html_table(celebs_array)
{

    //note: tr has id (used on delete)
    console.log("building html for table ...");
    var tr_array = [];
    var gender = "";
    for (var i = 0; i < celebs_array.length; i++)
    {
        gender = "";
        if (celebs_array[i].gender == "F") gender = "Female";
        if (celebs_array[i].gender == "M") gender = "Male";

        tr_array.push("<tr id="); tr_array.push(celebs_array[i].index);  tr_array.push(">");
        tr_array.push("<td>"); tr_array.push("<img src='"); tr_array.push(celebs_array[i].imageUri); tr_array.push("'</img>"); tr_array.push("</td>");
        tr_array.push("<td>"); tr_array.push(celebs_array[i].name); tr_array.push("</td>");
        tr_array.push("<td>"); tr_array.push(celebs_array[i].role); tr_array.push("</td>");
        tr_array.push("<td>"); tr_array.push(celebs_array[i].birthDate.split('T')[0]); tr_array.push("</td>");
        tr_array.push("<td>");tr_array.push(gender);tr_array.push("</td>");
        tr_array.push("<td>"); tr_array.push("<button onclick='on_delete_celeb("); tr_array.push(celebs_array[i].index); tr_array.push(")'>Delete</button>"); tr_array.push("</td>");
        tr_array.push("</tr>");

        if ((i % 10) == 0)
        {
            console.log("built " + i + " rows");
        }
    }
    console.log("building html for table ... done");
    var table_html = "<table><tr><th>image</th><th>name</th><th>role</th><th>birth date</th><th>gender</th><th>...</th></tr>" + tr_array.join("") + "</table>";
    return table_html;
}

function showSpinner()
{
    $("#divLoading").show();
}

function hideSpinner()
{
    $("#divLoading").hide();
}