<?php 
	include 'connection.php';
	$conn = OpenCon();
	$ChoreName_ = $_GET["ChoreName"];
	$CompleteBy_ = $_GET["CompleteBy"];
	$AssignedTo_ = $_GET["AssignedTo"];
	$Priority_ = $_GET["Priority"];
	$sql = "INSERT into chores (chore_title, complete_by_date, assigned_to, priority) Values ('$ChoreName_', '$CompleteBy_', '$AssignedTo_', '$Priority_')";
	if(mysqli_query($conn,$sql))
	{
		echo "successful to chores";
	}
	else
	{
		echo "error";
	}
	?>