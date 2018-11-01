<?php
	include 'connection.php';
	$conn = OpenCon();
	if($conn === false)
	{
		die("error" . mysqli_connect_error());
	}
	$sql = "INSERT into user (Email,FirstName,LastName,Password,Phone,Username) Values ('testfromapp@email.com', 'testfromapp', 'testfromapp', 'testfromapp', 'testfromapp', 'testfromapp')";
	if(mysqli_query($conn,$sql))
	{
		echo "insertion successful";
	}
	else
	{
		echo "error failed insertion";
	}
	CloseCon($conn);
?>