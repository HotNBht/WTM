﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Doc.Models;

namespace WalkingTec.Mvvm.Doc.ViewModels.StudentVms
{
    public class StudentListVm2 : BasePagedListVM<Student,StudentSearcher>
    {
        public StudentListVm2()
        {
            DetailGridPrix = "Students";
        }

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Employee", GridActionStandardTypesEnum.AddRow, ""),
                this.MakeStandardAction("Employee", GridActionStandardTypesEnum.RemoveRow, ""),

            };
        }

        protected override IEnumerable<IGridColumn<Student>> InitGridHeader()
        {
            return new List<GridColumn<Student>>{
                this.MakeGridHeader(x => x.LoginName).SetEditType(EditTypeEnum.TextBox),
                this.MakeGridHeader(x => x.Name).SetEditType(EditTypeEnum.TextBox),
                this.MakeGridHeader(x => x.Sex).SetEditType(EditTypeEnum.ComboBox, typeof(Models.SexEnum).ToListItems(pleaseSelect:true)),
                this.MakeGridHeader(x => x.CellPhone).SetEditType(EditTypeEnum.TextBox),
                this.MakeGridHeader(x=>x.IsValid).SetEditType(EditTypeEnum.CheckBox)
            };
        }

        public override IOrderedQueryable<Student> GetSearchQuery()
        {
            List<Student> data = new List<Student>
            {
                new Student{ LoginName = "zhangsan", Name="张三", Sex= Models.SexEnum.Male, CellPhone="13012213483", ExcelIndex = 0, IsValid = true, ID = new Guid("6F5C2D15-4871-4083-B269-06F456A4F1B6")},
                new Student{ LoginName = "lisi", Name="李四", Sex= Models.SexEnum.Male, CellPhone="13075829654", ExcelIndex = 1, IsValid = false, ID = new Guid("9C7BC358-B8BD-4547-AFC1-11BF6F2B608B")},
                new Student{ LoginName = "wangwu", Name="王五", Sex= Models.SexEnum.Male, CellPhone="13098635100", ExcelIndex = 2, IsValid = true, ID = new Guid("3BF9217C-1ACF-4D80-9899-42CFDA4C8746")},
                new Student{ LoginName = "zhaoliu", Name="赵六", Sex= Models.SexEnum.Female, CellPhone="13035698123", ExcelIndex = 3, IsValid = false, ID = new Guid("0C7F6A24-A08D-46BD-86AC-6B6A391A9F04")},
            };

            var query = data.AsQueryable().Where(x=>
                    (string.IsNullOrEmpty(Searcher.LoginName) || x.LoginName.Contains(Searcher.LoginName)) &&
                    (string.IsNullOrEmpty(Searcher.Name) || x.Name.Contains(Searcher.Name)) &&
                    (Searcher.Sex == null || x.Sex == Searcher.Sex) 
                )
                .OrderBy(x => x.ExcelIndex);
            return query;
        }

    }

}
