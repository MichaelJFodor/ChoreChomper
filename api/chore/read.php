<?php //returns TaskList
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

// include database and object files
include_once '../config/database.php';
include_once '../objects/chore.php';
 
// instantiate database and product object
$database = new Database();
$db = $database->getConnection();
 
// initialize object
$chore = new Chore($db);

// query products
$stmt = $chore->read();
$num = $stmt->rowCount();
 
// check if more than 0 record found
if($num>0){
 
    // products array
    $chore_arr=array();
    $chore_arr["records"]=array();
 
    // retrieve our table contents
    // fetch() is faster than fetchAll()
    // http://stackoverflow.com/questions/2770630/pdofetchall-vs-pdofetch-in-a-loop
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        // extract row
        // this will make $row['name'] to
        // just $name only
        extract($row);
 
        $chore_item=array(
            "choreId" => $c_ID,
            "choreName" => $chore_title,
            "deadlineTimestamp" => $complete_by_date,
			"completedUID" => $completed_by,
			"completedTimestamp" => $completed_date,
			"priority" => $priority,
			"assignedUserId" => $assigned_to
        );
 
        array_push($chore_arr["records"], $chore_item);
    }
 
    // set response code - 200 OK
    http_response_code(200);
 
    // show products data in json format
    echo json_encode($chore_arr);
}
 else{
 
    // set response code - 404 Not found
    http_response_code(404);
 
    // tell the user no products found
    echo json_encode(
        array("message" => "No chores found.")
    );
}