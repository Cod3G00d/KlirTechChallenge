
export class Promotion {
    id: string;
    name: string;
    active : boolean;
  
    constructor(id: string, name: string, active : boolean) {
      this.id = id;
      this.name = name;
      this.active = active;
    }
  }
  
  