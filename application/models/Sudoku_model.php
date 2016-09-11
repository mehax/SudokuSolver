<?php

/**
 * Created by PhpStorm.
 * User: mehaX
 * Date: 9/11/2016
 * Time: 3:08 PM
 */
class Sudoku_model extends CI_Model
{
    public function __construct()
    {
        $this->load->database();
    }

    public function get_sudokus()
    {
        $this->db->order_by('date', 'asc');
        return $this->db->get('sudoku')->result_array();
    }

    public function get_sudoku($sudoku_id)
    {
        return $this->db->get_where('sudoku', array('id' => $sudoku_id))->row_array();
    }

    public function insert($sudoku)
    {
        $this->db->insert('sudoku', $sudoku);
        return $this->db->insert_id();
    }
}