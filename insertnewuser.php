<?php
	include 'connection.php';
	$conn = OpenCon();
	if($conn === false)
	{
		die("error" . mysqli_connect_error());
	}
	$FirstName_ = $_GET["FirstName"];
	$Email_ = $_GET["Email"];
	$LastName_ = $_GET["LastName"];
	$Password_ = $_GET["Password"];
	$Phone_ = $_GET["Phone"];
	$Username_ = $_GET["Username"];
	echo "$Email_ $FirstName_ $LastName_ $Password_ $Phone_ $Username_";
	$sql = "INSERT into user (Email,FirstName,LastName,Password,Phone,Username) Values ('$Email_','$FirstName_','$LastName_','$Password_','$Phone_','$Username_')";
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