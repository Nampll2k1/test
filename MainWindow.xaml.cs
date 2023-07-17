using ReadFileJson.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ReadFileJson
{
    public partial class MainWindow : Window
    {
        private List<Employee> employees;
        private List<Department> departments;

        public MainWindow()
        {
            InitializeComponent();
            employees = new List<Employee>();
            departments = new List<Department>();
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            string filePath = @"employee.json";

            // Kiểm tra xem tệp JSON có tồn tại hay không
            if (File.Exists(filePath))
            {
                // Đọc nội dung từ tệp JSON
                string jsonContent = File.ReadAllText(filePath);

                // Phân tích dữ liệu từ JSON thành danh sách nhân viên
                employees = JsonSerializer.Deserialize<List<Employee>>(jsonContent);

                // Gán danh sách nhân viên cho ListView
                lvEmployee.ItemsSource = employees;
                departments = CreateSampleDepartments();
                cbDepartment.ItemsSource = departments;
                string[] arr = { "mrs", "mr" };
                cbCourtesy.ItemsSource = arr;
            }
            else
            {
                MessageBox.Show("File employee.json không tồn tại.");
            }
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có mục được chọn trong ListView hay không
            if (lvEmployee.SelectedItem != null)
            {
                // Lấy nhân viên đang được chọn
                Employee selectedEmployee = (Employee)lvEmployee.SelectedItem;

                // Cập nhật thông tin nhân viên từ TextBox và ComboBox
                selectedEmployee.EmployeeId = int.Parse(employeeId.Text);
                selectedEmployee.FirstName = firstName.Text;
                selectedEmployee.LastName = lastName.Text;
                selectedEmployee.Department.DepartmentName = cbDepartment.Text;
                selectedEmployee.Title = title.Text;
                selectedEmployee.TitleOfCourtesy = cbCourtesy.Text;
                selectedEmployee.BirthDate = dob.SelectedDate.Value;

                // Lưu danh sách nhân viên vào tệp JSON
                SaveEmployeesToJson();

                // Cập nhật ListView
                lvEmployee.Items.Refresh();
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            // Tạo một đối tượng nhân viên mới
            Employee newEmployee = new Employee
            {
                EmployeeId = int.Parse(employeeId.Text),
                FirstName = firstName.Text,
                LastName = lastName.Text,
                Department = new Department { DepartmentName = cbDepartment.Text },
                Title = title.Text,
                TitleOfCourtesy = cbCourtesy.Text,
                BirthDate = dob.SelectedDate.Value
            };

            // Thêm nhân viên mới vào danh sách
            employees.Add(newEmployee);

            // Lưu danh sách nhân viên vào tệp JSON
            SaveEmployeesToJson();

            // Cập nhật ListView
            lvEmployee.Items.Refresh();
        }
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có mục được chọn trong ListView hay không
            if (lvEmployee.SelectedItem != null)
            {
                // Lấy nhân viên đang được chọn
                Employee selectedEmployee = (Employee)lvEmployee.SelectedItem;

                // Xóa nhân viên khỏi danh sách
                employees.Remove(selectedEmployee);

                // Lưu danh sách nhân viên vào tệp JSON
                SaveEmployeesToJson();

                // Cập nhật ListView
                lvEmployee.Items.Refresh();
            }
        }

        private void SaveEmployeesToJson()
        {
            string filePath = @"C:\Users\PC\Desktop\ReadFileJson\ReadFileJson\employee.json";

            // Chuyển danh sách nhân viên thành chuỗi JSON
            string jsonContent = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });

            // Ghi nội dung JSON vào tệp
            File.WriteAllText(filePath, jsonContent);
        }

        private void lvEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvEmployee.SelectedItem != null)
            {
                Employee selectedEmployee = (Employee)lvEmployee.SelectedItem;
                employeeId.Text = selectedEmployee.EmployeeId.ToString();
                firstName.Text = selectedEmployee.FirstName;
                lastName.Text = selectedEmployee.LastName;
                cbDepartment.Text = selectedEmployee.Department.DepartmentName;
                title.Text = selectedEmployee.Title;
                cbCourtesy.Text = selectedEmployee.TitleOfCourtesy;
                dob.SelectedDate = selectedEmployee.BirthDate;
            }
        }


        private void CreateSampleData()
        {
            employees = CreateSampleEmployees();
            departments = CreateSampleDepartments();
        }

        private static List<Employee> CreateSampleEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                DepartmentId = 1,
                Title = "Software Engineer",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1990, 5, 15),
                HireDate = new DateTime(2015, 10, 1),
                Address = "123 Main St"
            });

            employees.Add(new Employee
            {
                EmployeeId = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DepartmentId = 2,
                Title = "HR Manager",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1985, 10, 25),
                HireDate = new DateTime(2010, 3, 15),
                Address = "456 Elm St"
            });

            employees.Add(new Employee
            {
                EmployeeId = 3,
                FirstName = "Mike",
                LastName = "Johnson",
                DepartmentId = 3,
                Title = "Accountant",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1988, 7, 12),
                HireDate = new DateTime(2018, 6, 1),
                Address = "789 Oak St"
            });

            return employees;
        }

        private static List<Department> CreateSampleDepartments()
        {
            List<Department> departments = new List<Department>();

            departments.Add(new Department
            {
                DepartmentId = 1,
                DepartmentName = "IT",
                DepartmentType = "Technical"
            });

            departments.Add(new Department
            {
                DepartmentId = 2,
                DepartmentName = "HR",
                DepartmentType = "Administrative"
            });

            departments.Add(new Department
            {
                DepartmentId = 3,
                DepartmentName = "Finance",
                DepartmentType = "Financial"
            });

            return departments;
        }


    }
}
