import { useEffect, useState } from "react";
import type { TodoItem } from "./types/todo";

function TodoList() {
    const [todos, setTodos] = useState<TodoItem[]>([]);
    const [newTodo, setNewTodo] = useState('');
    const API_URL = import.meta.env.VITE_API_URL;

    const fetchTodos = () => {
        fetch(`${API_URL}/todo`, {})
        .then(res => res.json())
        .then(data=> setTodos(data))
        .catch(err => console.error('Error fetching todos:', err));
    };

    const addTodo = () => {
        if(!newTodo.trim()) return;
        fetch(`${API_URL}/todo`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({ title: newTodo})
        })
            .then(data => {
                console.log("create todo:", data);
                setNewTodo('');
                fetchTodos();
            })
            .catch(err => {
                console.error("Error in add todo:", err);
            })
    }
    
    const toggleComplete = (todo: TodoItem) => {
        const updateTodo = {
            ...todo,
            isCompleted: !todo.isCompleted
        };

        fetch(`${API_URL}/todo/${todo.id}`, {
            method: 'PUT',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(updateTodo)
        })
        .then(res => {
            if(!res.ok) throw new Error(`Failed to update todo ${todo.id}`);
            fetchTodos();
        })
        .catch(err => console.error("Error updating todo:", err));
    };

    const deleteTodo = (id: number) => {
        fetch(`${API_URL}/todo/${id}`, {
            method: 'DELETE'
        })
        .then(res => {
            if(!res.ok) throw new Error(`Failed to delete todo ${id}`);
            fetchTodos();
        })
        .catch(err => console.error("Error deleting todo:", err));
    }


    useEffect(() => {
        fetchTodos();
        console.log("API URL:", API_URL);
    }, []);

    return (
        <div>
            <h1>My To-Do List</h1>

            <div>
                <input 
                type="text"
                value={newTodo}
                placeholder="Add a task"
                onChange={(e) => setNewTodo(e.target.value)}/>
                <button onClick={addTodo}>Add</button>
            </div>

            <ul>
                {todos.map(todo => (
                    <ul key={todo.id}>
                        <input type="checkbox" checked={todo.isCompleted} onChange={() => toggleComplete(todo)} />
                        {todo.title}
                        <button onClick={() => deleteTodo(todo.id)} style={{marginLeft: '1rem'}}>Delete</button>
                    </ul>
                ))}
            </ul>
        </div>
    )
}

export default TodoList;