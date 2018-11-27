<?php
	include 'connection.php';
	$conn = OpenCon();	
	$name = $_GET["Group_name"];
	$password = $_GET["Group_password"];
	$sql = "INSERT into chorechomper.group (nameGroup, groupPassword) Values ('$name','$password')";
	
	if(mysqli_query($conn,$sql))
	{
		echo json_encode('true');
	}
	else
	{
		echo json_encode('false');
	}
	CloseCon($conn);
?>
