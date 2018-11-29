<?php
	//this file joins a group given the group parameters
	include 'connection.php';
	$conn = OpenCon();

	$name = $_GET["Group_name"];
	$password = $_GET["Group_password"];
	$uid = $_GET["User_id"];
	
	$getGid_sql = ("SELECT idGroup FROM chorechomper.group WHERE nameGroup = '$name' AND groupPassword = '$password'");
	$result=mysqli_query($conn,$getGid_sql);
	$row = mysqli_fetch_assoc($result);
	$gid = $row['idGroup'];

	if($result->num_rows != 0)
	{
		$enter_into_relational = "INSERT INTO chorechomper.group_has_users (id_group, user_id) VALUES ('$gid','$uid')";
		mysqli_query($conn,$enter_into_relational);
		echo json_encode($gid);
	}
	
	else
		echo json_encode('-1');
	
	CloseCon($conn);
	?>
