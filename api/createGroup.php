<?php
	//this file takes in two parameters and creates a new group in the db
	include 'connection.php';
	$conn = OpenCon();	
	$name = $_GET["Group_name"];
	$password = $_GET["Group_password"];
	
	$create_group_sql = "INSERT into chorechomper.group (nameGroup, groupPassword) Values ('$name','$password')";
	
	if(mysqli_query($conn,$create_group_sql))
	{
		echo json_encode('true');
	}
	else
	{
		echo json_encode('false');
	}
	CloseCon($conn);
?>
