<?php
	//this file creates a new user
	//it also needs to create a default group 
	//it also will create a default chore 
	
	include 'connection.php';
	$conn = OpenCon();

	$FirstName_ = $_GET["FirstName"];
	$Email_ = $_GET["Email"];
	$LastName_ = $_GET["LastName"];
	$Password_ = $_GET["Password"];
	$Phone_ = $_GET["Phone"];
	$Username_ = $_GET["Username"];
	
	$default = 'default';
	$mkGroup = ' Make Group';
	$firstChore = $Username_ . $mkGroup;
	$combined_key = $Username_ . $default;
	$combined_password = $Username_ . $Password_;
	//queries 
	$new_user_sql = "INSERT into chorechomper.user (Email,FirstName,LastName,Password,Phone,Username) Values ('$Email_','$FirstName_','$LastName_','$Password_','$Phone_','$Username_')";
	$new_group_sql = "INSERT into chorechomper.group (nameGroup, groupPassword) Values ('$combined_key','$combined_password')";
	$get_uid = "SELECT user_id FROM chorechomper.user WHERE Username = '$Username_'";
	
	if(mysqli_query($conn,$new_user_sql))
	{
		if(mysqli_query($conn,$new_group_sql))
		{
			$dummyTitle = 'MakeGroup';
			$dummyTime = 'now';
			$dummyPri = '1';
			
			//need to get the user id
			$u_result = mysqli_query($conn,$get_uid);
			$row_u = mysqli_fetch_assoc($u_result);
			$uid = $row_u['user_id'];
			
			//need to get the group id
			$get_gid = "SELECT idGroup FROM chorechomper.group WHERE nameGroup = '$combined_key'";
			$g_result = mysqli_query($conn,$get_gid);
			$row_g = mysqli_fetch_assoc($g_result);
			$gid = $row_g['idGroup'];
			
			//add group and the user to the group has users db
			$new_group_has_users_sql = "INSERT into chorechomper.group_has_users (id_group, user_id) VALUES ('$gid', '$uid')";
			mysqli_query($conn,$new_group_has_users_sql);
			
			//creates a default chore that is hardcoded except for the name
			$a="INSERT INTO chorechomper.chores (chore_title, complete_by_date, priority, assigned_to) VALUES ('$firstChore', '0/0/0000', '1', '$uid')";
			mysqli_query($conn,$a);
			
			//gets the chore id
			$get_chore_id = "SELECT c_ID FROM chorechomper.chores WHERE chore_title = '$firstChore' AND complete_by_date = '0/0/0000' AND assigned_to = '$uid' AND priority = '1'";
           		$c_result = mysqli_query($conn,$get_chore_id);
           		$row_c = mysqli_fetch_assoc($c_result);
			$cid = $row_c['c_ID'];
			
			//enter in the chore id and the user id t othe relational database
			$new_user_chore = "INSERT into chorechomper.user_has_chores (user_id, chore_id) Values ('$uid', '$cid')";
			mysqli_query($conn,$new_user_chore);
			echo json_encode($uid);
		}
	}
	CloseCon($conn);
?>
