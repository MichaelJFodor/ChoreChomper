<?php
	//this file makes sure the login credentials are valid
	include 'connection.php';
	$conn = OpenCon();

	$username = $_GET["Username_Attempt"];
	$password = $_GET["Password_Attempt"];
	$login_check_sql = ("SELECT user_id FROM user WHERE Password = '$password' AND Username = '$username'");
	$result=mysqli_query($conn,$login_check_sql);

	if($result->num_rows != 0)
	{
		echo json_encode("true");
	}
	
	CloseCon($conn);
?>
