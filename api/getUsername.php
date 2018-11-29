<?php
	//this file gets the username of a user given the user id
	include 'connection.php';
	$conn = OpenCon();
	$uid = $_GET["Id"];
	$getUsername_sql = ("SELECT Username FROM chorechomper.user WHERE user_id = '$uid'");
	$result = mysqli_query($conn,$getUsername_sql);
	
	if($result->num_rows == 1)
	{
		$row = mysqli_fetch_assoc($result);
		$username = $row['Username'];
		echo json_encode($username);
	}
	else
		echo json_encode('-1');
	CloseCon($conn);
	?>
