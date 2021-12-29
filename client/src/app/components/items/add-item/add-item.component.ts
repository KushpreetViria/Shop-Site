import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserDetailsService } from 'src/app/_services/user-details.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {
  itemInfoForm: FormGroup;
  selectedFile : File = null
  inAddingMode : boolean;
  submitted = false;
  fileError = {error:false,cause:''};


  constructor(
    private formBuilder : FormBuilder,
    private route : ActivatedRoute,
    private router : Router,
    private userService : UserDetailsService,
    private toastrService : ToastrService) 
    { 
    }

  ngOnInit(): void {
    this.inAddingMode = true;
    var currencyRegex = "^\\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$";

    this.itemInfoForm = this.formBuilder.group({
      Name: ['',[Validators.required,Validators.maxLength(50)]],
      Price: [,[Validators.required,Validators.pattern(currencyRegex)]],
      Description: ['',[Validators.required, Validators.minLength(50), Validators.maxLength(1000)]]
    });

  }

  get form() { return this.itemInfoForm.controls; }

  onFileSelected(e : Event){    
    var target = (e.target as HTMLInputElement)
    var file = target.files[0];
    if(file.type === "image/jpeg" || file.type  === "image/png"){
      if(file.size > 512000){
        this.fileError = {
          error: true,
          cause:'fileSize'
        };
      }else{
        this.selectedFile = target.files[0];
        this.fileError = {error:false,cause:''};
      }
    }else{
      this.fileError = {
        error: true,
        cause: 'fileType'
      };
    }
  }

  onSubmit(){    
    this.submitted = true;

    if(this.fileError.error || this.itemInfoForm.invalid) return;

    const fd = new FormData();
    
    fd.append('item',JSON.stringify(this.itemInfoForm.value));
    if(this.selectedFile) 
      fd.append('image',this.selectedFile,this.selectedFile.name);    

    console.log(JSON.stringify(this.itemInfoForm.value))
    this.userService.addItem(fd).subscribe(res => {
      this.itemInfoForm.reset;
    });
  }
}
