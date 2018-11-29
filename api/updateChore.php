<?php
	//this files updates the chores given the new data
	include 'connection.php';
	$conn = OpenCon();

	$ChoreName_ = $_GET["ChoreName"];
	$CompleteBy_ = $_GET["CompleteBy"];
	$AssignedTo_ = $_GET["AssignedTo"];
	$Priority_ = $_GET["Priority"];
	$chore_id_ = $_GET["ChoreId"];
	$updateChore_sql = "UPDATE chores SET complete_by_date = '$CompleteBy_', chore_title = '$ChoreName_', priority = '$Priority_', assigned_to = '$AssignedTo_' WHERE (c_ID = '$chore_id_')";
	
	if(mysqli_query($conn,$updateChore_sql))
	{
		echo json_encode('success');
	}
	
	else
		echo json_encode('-1');
	
	CloseCon($conn);
?>
