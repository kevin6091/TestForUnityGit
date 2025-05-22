using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmployeeManager : BaseManager
{
    private Queue<EmployeeController> Employees { get; } = new Queue<EmployeeController>();

    public EmployeeController TryPeekEmployees { get { return Employees.Count() > 0 ? Employees.Peek() : null; } }
    public EmployeeController DeQueueEmployees { get { return Employees.Dequeue(); } }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@Employee_Root");
            if (root == null)
                root = new GameObject { name = "@Employee_Root" };
            return root;
        }
    }

    public void GenerateEmployee()
    {
        AddEmployee(InstantiateEmployee());
    }

    public void AddEmployee(EmployeeController employee)
    {
        Employees.Enqueue(employee);
    }

    private EmployeeController InstantiateEmployee()
    {
        GameObject gameObject = Managers.Resource.Instantiate($"Employee/Employee");
        gameObject.transform.SetParent(Root.transform);

        return gameObject.GetOrAddComponent<EmployeeController>();
    }
}
