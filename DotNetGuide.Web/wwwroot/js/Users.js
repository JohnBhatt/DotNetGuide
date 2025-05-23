﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/user/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'city', "width": "08%" },
            { data: 'state', "width": "08%" },
            { data: 'postalCode', "width": "08%" },
            { data: 'companyName', "width": "10%" },
            { data: 'role', "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:120px" >
                                <i class="bi bi-lock-fill"></i>Locked
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer; width:140px" href="/User/RoleManagement?userID=${data.id}">
                                <i class="bi bi-pencil-square"></i>Permission
                            </a>
                        </div>
                        `
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:120px">
                                <i class="bi bi-unlock-fill"></i>Unlocked
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer; width:140px" href="/User/RoleManagement?userID=${data.id}">
                                <i class="bi bi-pencil-square"></i>Permission
                            </a>
                        </div>
                        `

                    }

                }

            }
        ]
    });
}


function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}