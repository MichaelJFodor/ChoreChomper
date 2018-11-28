<?php
	include 'connection.php';
	$conn = OpenCon();
	$ChoreName_ = $_GET["ChoreName"];
	$CompleteBy_ = $_GET["CompleteBy"];
	$AssignedTo_ = $_GET["AssignedTo"];
	$Priority_ = $_GET["Priority"];
	$chore_id_ = $_GET["ChoreId"];
	$completed_by_person_ = $_GET["completed_by_person"];
	$completed_date_ = $_GET["completed_date"];
	$sql = "UPDATE chores SET complete_by_date = '$CompleteBy_', chore_title = '$ChoreName_', completed_by = '$CompleteBy_', completed_date = '$completed_date_', priority = '$Priority_', assigned_to = '$AssignedTo_' WHERE (chore_id = '$chore_id_')";
	if(mysqli_query($conn,$sql))
	{
		echo json_encode('success');
	}
	else
	{
		echo json_encode('-1');
	}
	CloseCon($conn);
?>