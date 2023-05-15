using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Store_System
{
    public partial class Form2 : Form
    {
        Store_SystemEntities Context = new Store_SystemEntities();
        public Form2()
        {
            InitializeComponent();
            //store
            var query = from store in Context.Stores
                        select new { store.Id };
            foreach (var item in query)
            {
                cbStore.Items.Add(item.Id);
                cbStoreId.Items.Add(item.Id);
                cbExchangeStoreId.Items.Add(item.Id);
                cbSupplyStoreId.Items.Add(item.Id);
                cbStorageReportId.Items.Add(item.Id);
                cbTransferNewtStoreId.Items.Add(item.Id);
            }
            //product
            var query1 = from product in Context.Products
                         select new { product.Id };
            foreach (var item in query1)
            {
                cbProductId.Items.Add(item.Id);
                cbReportProductId2.Items.Add(item.Id);
                checkedListBox2.Items.Add(item.Id);
                checkedListBox3.Items.Add(item.Id);
                checkedListBox4.Items.Add(item.Id);
                //cbExchangeProductId.Items.Add(item.Id);
                //cbSupplyProductId.Items.Add(item.Id);

            }


            //Customer
            var query2 = from customer in Context.Customers
                         select new { customer.Id };
            foreach (var item in query2)
            {
                cbCustomerId.Items.Add(item.Id);
                cbExchangeCustomerId.Items.Add(item.Id);
            }
            //Supplier
            var query3 = from supplier in Context.Suppliers
                         select new { supplier.Id };
            foreach (var item in query3)
            {
                cbSupplierId.Items.Add(item.Id);
                cbSupplySupplierId.Items.Add(item.Id);
            }

            //Exchange 
            var query4 = from exchange in Context.Exchange_Permit
                         select new { exchange.Id };
            foreach (var item in query4)
            {
                cbExchangeId.Items.Add(item.Id);
            }

            //Supply
            var query5 = from supply in Context.Supply_Permit
                         select new { supply.Id };
            foreach (var item in query5)
            {
                cbSupplyId.Items.Add(item.Id);
                cbTransferPremissionId.Items.Add(item.Id);
            }

            //Report3,4,5
            //var storeList = (from store in Context.Supply_Permit
            //                 select new { store.Storage_Id });
            //foreach (var store in storeList)
            //{
            //    checkedListBox2.Items.Add(store.Storage_Id);
            //    checkedListBox3.Items.Add(store.Storage_Id);
            //    checkedListBox4.Items.Add(store.Storage_Id);
            //}


        }

        //Storage
        private void btnAddStorage_Click(object sender, EventArgs e)
        {
            string address = tbStoreAddress.Text;
            string manager = tbStoreIdManager.Text;
            string name = tbStorageName.Text;

            Store store = new Store { Name = name, Address = address, Manager = manager };

            Context.Stores.Add(store);
            MessageBox.Show("Inserted");
            Context.SaveChanges();
        }
        private void btnUpdateStorage_Click(object sender, EventArgs e)
        {
            if (cbStoreId.SelectedItem != null)
            {
                int.TryParse(cbStoreId.SelectedItem.ToString(), out int id);
                var query = (from store in Context.Stores
                             where store.Id == id
                             select store).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Address = tbStoreAddress.Text;
                query.Manager = tbStoreIdManager.Text;
                query.Name = tbStorageName.Text;
                Context.SaveChanges();
            }
            else MessageBox.Show("Select Id");
        }
        private void cbStoreId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbStoreId.SelectedItem.ToString(), out int id);

            if (cbStoreId.SelectedItem != null)
            {
                var query = (from store in Context.Stores
                             where store.Id == id
                             select store).FirstOrDefault();

                tbStoreAddress.Text = query.Address;
                tbStoreIdManager.Text = query.Manager;
                tbStorageName.Text = query.Name;

            }
            else
            {
                tbStoreAddress.Text = "";
                tbStoreIdManager.Text = "";
            }
        }


        //Product
        private void button2_Click(object sender, EventArgs e)
        {
            string name = tbProductName.Text;
            string unit = tbProductUnit.Text;




            Product product = new Product { Name = name, Unit = unit };
            var currentStore = Context.Stores.Where((s) => s.Id == (int)cbStore.SelectedItem).FirstOrDefault();
            product.Stores.Add(currentStore);
            Context.Products.Add(product);
            MessageBox.Show("Inserted");
            Context.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbProductId.SelectedItem != null)
            {
                int.TryParse(cbProductId.SelectedItem.ToString(), out int id);
                var query = (from product in Context.Products
                             where product.Id == id
                             select product).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Name = tbProductName.Text;
                query.Unit = tbProductUnit.Text;
                Context.SaveChanges();
            }
            else MessageBox.Show("Enter Id");
        }

        private void cbProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbProductId.SelectedItem.ToString(), out int id);

            if (cbProductId.SelectedItem != null)
            {
                var query = (from product in Context.Products
                             where product.Id == id
                             select product).FirstOrDefault();
                if (query != null && id != 0)
                {
                    tbProductName.Text = query.Name;
                    tbProductUnit.Text = query.Unit;
                    cbStore.SelectedItem = query.Stores.FirstOrDefault().Id;
                    tbStoreName1.Text = query.Stores.FirstOrDefault().Name;
                }

            }
            else
            {
                tbProductName.Text = "";
                tbProductUnit.Text = "";
                cbStore.SelectedItem = null;
            }
        }
        private void cbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)cbStore.SelectedItem;
            var query = (from store in Context.Stores
                         where store.Id == id
                         select store.Name).FirstOrDefault();

            tbStoreName1.Text = query;
        }


        //Customer
        private void button4_Click(object sender, EventArgs e)
        {
            string name = tbCustomerName.Text;
            string email = tbCustomerEmail.Text;
            string mobile = tbCustomerMobile.Text;
            string fax = tbCustomerFax.Text;
            string website = tbCustomerWebsite.Text;

            Customer customer = new Customer { Name = name, Email = email, Mobile = mobile, Fax = fax, Website = website };
            Context.Customers.Add(customer);
            MessageBox.Show("Inserted");
            Context.SaveChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cbCustomerId.SelectedItem != null)
            {
                int.TryParse(cbCustomerId.SelectedItem.ToString(), out int id);
                var query = (from customer in Context.Customers
                             where customer.Id == id
                             select customer).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Name = tbCustomerName.Text;
                query.Email = tbCustomerEmail.Text;
                query.Mobile = tbCustomerMobile.Text;
                query.Fax = tbCustomerFax.Text;
                query.Website = tbCustomerWebsite.Text;
                Context.SaveChanges();
            }
            else MessageBox.Show("Select Id");
        }

        private void cbCustomerId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbCustomerId.SelectedItem.ToString(), out int id);

            if (cbCustomerId.SelectedItem != null)
            {
                var query = (from customer in Context.Customers
                             where customer.Id == id
                             select customer).FirstOrDefault();
                if (query != null && id != 0)
                {
                    tbCustomerName.Text = query.Name;
                    tbCustomerEmail.Text = query.Email;
                    tbCustomerMobile.Text = query.Mobile;
                    tbCustomerFax.Text = query.Fax;
                    tbCustomerWebsite.Text = query.Website;
                }

            }
            else
            {
                tbCustomerName.Text = tbCustomerEmail.Text = tbCustomerMobile.Text = tbCustomerFax.Text = tbCustomerWebsite.Text = "";
            }
        }



        //Supplier
        private void button6_Click(object sender, EventArgs e)
        {
            string name = tbSupplierName.Text;
            string email = tbSupplierEmail.Text;
            string mobile = tbSupplierMobile.Text;
            string fax = tbSupplierFax.Text;
            string website = tbSupplierWebsite.Text;

            Supplier supplier = new Supplier { Name = name, Email = email, Mobile = mobile, Fax = fax, Website = website };
            Context.Suppliers.Add(supplier);
            MessageBox.Show("Inserted");
            Context.SaveChanges();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cbSupplierId.SelectedItem != null)
            {
                int.TryParse(cbSupplierId.SelectedItem.ToString(), out int id);
                var query = (from supplier in Context.Suppliers
                             where supplier.Id == id
                             select supplier).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Name = tbSupplierName.Text;
                query.Email = tbSupplierEmail.Text;
                query.Mobile = tbSupplierMobile.Text;
                query.Fax = tbSupplierFax.Text;
                query.Website = tbSupplierWebsite.Text;
                Context.SaveChanges();
            }
            else MessageBox.Show("Enter Id");
        }

        private void cbSupplierId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbSupplierId.SelectedItem.ToString(), out int id);

            if (cbSupplierId.SelectedItem != null)
            {
                var query = (from supplier in Context.Suppliers
                             where supplier.Id == id
                             select supplier).FirstOrDefault();
                if (query != null && id != 0)
                {
                    tbSupplierName.Text = query.Name;
                    tbSupplierEmail.Text = query.Email;
                    tbSupplierMobile.Text = query.Mobile;
                    tbSupplierFax.Text = query.Fax;
                    tbSupplierWebsite.Text = query.Website;
                }

            }
            else
            {
                tbSupplierName.Text = tbSupplierEmail.Text = tbSupplierMobile.Text = tbSupplierFax.Text = tbSupplierWebsite.Text = "";
            }
        }


        //Exchange Premit
        Exchange_Permit exchange_permit = new Exchange_Permit();
        private void button8_Click(object sender, EventArgs e)
        {
            int storageId = (int)cbExchangeStoreId.SelectedItem;
            int customerId = (int)cbExchangeCustomerId.SelectedItem;
            DateTime date = Convert.ToDateTime(tbExchangeDate.Text);

            exchange_permit.Storage_Id = storageId;
            exchange_permit.Customer_Id = customerId;
            exchange_permit.Date = date;



            MessageBox.Show("Added Permission Select Item");
        }
        private void button11_Click(object sender, EventArgs e)
        {
            int productId = (int)cbExchangeProductId.SelectedItem;
            int quantity = Convert.ToInt32(tbExchangeQuantity.Text);
            exchange_permit.Product_Id = productId;
            exchange_permit.Quantity = quantity;

            Context.Exchange_Permit.Add(exchange_permit);
            Context.SaveChanges();
            MessageBox.Show("Inserted");
        }

        private void cbExchangeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbExchangeId.SelectedItem.ToString(), out int id);

            if (cbExchangeId.SelectedItem != null)
            {
                var query = (from exchange in Context.Exchange_Permit
                             where exchange.Id == id
                             select exchange).FirstOrDefault();
                if (query != null && id != 0)
                {
                    cbExchangeProductId.SelectedItem = query.Product_Id;
                    cbExchangeStoreId.SelectedItem = query.Storage_Id;
                    tbStoreName2.Text = query.Store.Name;
                    cbExchangeCustomerId.SelectedItem = query.Customer_Id;
                    tbExchangeDate.Text = query.Date.ToString();
                    tbExchangeQuantity.Text = query.Quantity.ToString();
                    tbProductName1.Text = query.Product.Name.ToString();
                }

            }
            else
            {
                cbExchangeProductId.Text = cbExchangeStoreId.Text = cbExchangeCustomerId.Text = tbExchangeDate.Text = tbExchangeQuantity.Text = "";
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (cbExchangeId.SelectedItem != null)
            {
                int.TryParse(cbExchangeId.SelectedItem.ToString(), out int id);
                var query = (from exchange in Context.Exchange_Permit
                             where exchange.Id == id
                             select exchange).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Product_Id = (int)cbExchangeProductId.SelectedItem;
                query.Storage_Id = (int)cbExchangeStoreId.SelectedItem;
                query.Customer_Id = (int)cbExchangeCustomerId.SelectedItem;
                query.Date = Convert.ToDateTime(tbExchangeDate.Text);
                query.Quantity = Convert.ToInt32(tbExchangeQuantity.Text);
                Context.SaveChanges();
            }
            else MessageBox.Show("Select Id");
        }

        private void cbExchangeStoreId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbExchangeProductId.Items.Clear();
            cbExchangeProductId.Text = "";

            var query = (from store in Context.Stores
                         where store.Id == (int)cbExchangeStoreId.SelectedItem
                         select store.Products).FirstOrDefault();
            foreach (var item in query)
            {

                {
                    cbExchangeProductId.Items.Add(item.Id);
                }
            }

            var query1 = (from store in Context.Stores
                          where store.Id == (int)cbExchangeStoreId.SelectedItem
                          select store.Name).FirstOrDefault();

            tbStoreName2.Text = query1;
        }
        private void cbExchangeProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = (from product in Context.Products
                         where product.Id == (int)cbExchangeProductId.SelectedItem
                         select product).FirstOrDefault();

            tbProductName1.Text = query.Name.ToString();

        }

        //Supplier Premit
        Supply_Permit supply_permit = new Supply_Permit();

        private void button10_Click(object sender, EventArgs e)
        {

            int storageId = (int)cbSupplyStoreId.SelectedItem;
            int supplierId = (int)cbSupplySupplierId.SelectedItem;
            DateTime date = Convert.ToDateTime(tbSupplyDate.Text);

            supply_permit.Storage_Id = storageId;
            supply_permit.Supplier_Id = supplierId;
            supply_permit.Date = date;


            MessageBox.Show("Presmission Added inset Items");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            int productId = (int)cbSupplyProductId.SelectedItem;
            int quantity = Convert.ToInt32(tbSupplyQuantity.Text);
            DateTime productionDate = Convert.ToDateTime(tbSupplyProductionDate.Text);
            DateTime expirationDate = Convert.ToDateTime(tbSupplyExpirationDate.Text);

            supply_permit.Product_Id = productId;
            supply_permit.Quantity = quantity;
            supply_permit.Production_Date = productionDate;
            supply_permit.Expiration_Date = expirationDate;

            Context.Supply_Permit.Add(supply_permit);
            Context.SaveChanges();
            MessageBox.Show("Item Inserted");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (cbSupplyId.Text != "")
            {
                int id = Int32.Parse(cbSupplyId.Text);
                if (id == 0) MessageBox.Show("Failed");
                var query = (from supply in Context.Supply_Permit
                             where supply.Id == id
                             select supply).FirstOrDefault();
                if (query == null) MessageBox.Show("Failed");
                else MessageBox.Show("Updated");
                query.Product_Id = Convert.ToInt32(cbSupplyProductId.Text);
                query.Storage_Id = Convert.ToInt32(cbSupplyStoreId.Text);
                query.Supplier_Id = Convert.ToInt32(cbSupplySupplierId.Text);
                query.Date = Convert.ToDateTime(tbSupplyDate.Text);
                query.Quantity = Convert.ToInt32(tbSupplyQuantity.Text);
                query.Production_Date = Convert.ToDateTime(tbSupplyProductionDate.Text);
                query.Expiration_Date = Convert.ToDateTime(tbSupplyExpirationDate.Text);
                Context.SaveChanges();
            }
            else MessageBox.Show("Enter Id");
        }

        private void cbSupplyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbSupplyId.SelectedItem.ToString(), out int id);

            if (cbSupplyId.SelectedItem != null)
            {
                var query = (from supply in Context.Supply_Permit
                             where supply.Id == id
                             select supply).FirstOrDefault();
                if (query != null && id != 0)
                {
                    cbSupplyProductId.SelectedItem = query.Product_Id;
                    cbSupplyStoreId.SelectedItem = query.Storage_Id;
                    tbStoreName3.Text = query.Store.Name;
                    cbSupplySupplierId.SelectedItem = query.Supplier_Id;
                    tbSupplyDate.Text = query.Date.ToString();
                    tbSupplyQuantity.Text = query.Quantity.ToString();
                    tbSupplyProductionDate.Text = query.Date.ToString();
                    tbSupplyExpirationDate.Text = query.Date.ToString();
                    tbProductName2.Text = query.Product.Name;
                }

            }
            else
            {
                cbSupplyProductId.Text = cbSupplyStoreId.Text = cbSupplySupplierId.Text = tbSupplyDate.Text = tbSupplyQuantity.Text = tbSupplyProductionDate.Text = tbSupplyExpirationDate.Text = "";
            }
        }

        private void cbSupplyStoreId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSupplyProductId.Items.Clear();
            cbSupplyProductId.Text = "";
            var query = (from store in Context.Stores
                         where store.Id == (int)cbSupplyStoreId.SelectedItem
                         select store.Products).FirstOrDefault();
            foreach (var item in query)
            {

                {
                    cbSupplyProductId.Items.Add(item.Id);
                }
            }
            var query1 = (from store in Context.Stores
                          where store.Id == (int)cbSupplyStoreId.SelectedItem
                          select store.Name).FirstOrDefault();

            tbStoreName3.Text = query1;
        }

        private void cbSupplyProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = (from product in Context.Products
                         where product.Id == (int)cbSupplyProductId.SelectedItem
                         select product).FirstOrDefault();

            tbProductName2.Text = query.Name.ToString();
        }

        //Transform
        private void cbTransferPremissionId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cbTransferPremissionId.SelectedItem.ToString(), out int id);

            if (cbTransferPremissionId.SelectedItem != null)
            {
                var query = (from supply in Context.Supply_Permit
                             where supply.Id == id
                             select supply).FirstOrDefault();
                if (query != null && id != 0)
                {
                    cbTransferProductId.Items.Clear();
                    cbTransferCurrentStoreId.Items.Clear();
                    cbTransferProductId.Items.Add(query.Product_Id);
                    cbTransferProductId.SelectedItem = query.Product_Id;
                    cbTransferCurrentStoreId.Items.Add(query.Storage_Id);
                    cbTransferCurrentStoreId.SelectedItem = query.Storage_Id;
                    tbTransferCurrentStoreName.Text = query.Store.Name;
                    tbProductName3.Text = query.Product.Name;
                }

            }
            else
            {
                cbSupplyProductId.Text = cbSupplyStoreId.Text = cbSupplySupplierId.Text = tbSupplyDate.Text = tbSupplyQuantity.Text = tbSupplyProductionDate.Text = tbSupplyExpirationDate.Text = "";
            }
        }

        private void cbTransferNewtStoreId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)cbTransferNewtStoreId.SelectedItem;
            var query = (from store in Context.Stores
                         where store.Id == id
                         select store).FirstOrDefault();
            if (query!=null && id != 0)
            {
                tbTransferNewStoreName.Text = query.Name;
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (cbTransferPremissionId.Text != "")
            {


                int id = (int)cbTransferPremissionId.SelectedItem;
                var query = (from supply in Context.Supply_Permit
                             where supply.Id == id
                             select supply).FirstOrDefault();
                if (query !=null)
                {
                    int.TryParse(tbTransferQuantity.Text, out int quantity);
                    if (tbTransferQuantity.Text != "" && tbTransferDate.Text != "" && quantity!= 0)
                    {

                        query.Quantity = query.Quantity - quantity;
                        Context.SaveChanges();
                        Supply_Permit supply = query;
                        supply.Storage_Id = (int)cbTransferNewtStoreId.SelectedItem;
                        supply.Product_Id = (int)cbTransferProductId.SelectedItem;
                        supply.Quantity = quantity;
                        supply.Date = Convert.ToDateTime(tbTransferDate.Text);
                        supply.Supplier_Id = query.Supplier_Id;
                        supply.Production_Date = query.Production_Date;
                        supply.Expiration_Date = query.Expiration_Date;

                        Context.Supply_Permit.Add(supply);
                        Context.SaveChanges();
                        MessageBox.Show("Transfered");
                    }
                }

            }

        }



        //Report 1
        private void button13_Click(object sender, EventArgs e)
        {
            if (cbStorageReportId.Text != "" && tbStorageReportDateF.Text != "" && tbStorageReportDateT.Text != "")
            {
                DateTime dateF = Convert.ToDateTime(tbStorageReportDateF.Text);
                DateTime dateT = Convert.ToDateTime(tbStorageReportDateT.Text);

                var query = (from supply in Context.Supply_Permit
                             join exchange in Context.Exchange_Permit
                             on new { supply.Product_Id, supply.Storage_Id } equals new { exchange.Product_Id, exchange.Storage_Id }
                             into currentGroup
                             from exchange in currentGroup.DefaultIfEmpty()
                             from prod in Context.Products
                             where supply.Storage_Id == (int)cbStorageReportId.SelectedItem
                                    && supply.Product_Id == prod.Id
                                    && supply.Date >= dateF
                                    && supply.Date <= dateT
                             select new
                             {
                                 supply.Product_Id,
                                 prod.Name,
                                 Quantity = supply.Quantity - (exchange.Quantity!=null ? exchange.Quantity : 0),
                                 supply.Date
                             });

                if (query!= null && query.Count() > 0)
                {
                    dgvReport1.DataSource = query.ToList();
                }
                else
                {
                    MessageBox.Show("Wrong Data");
                }

            }
            else MessageBox.Show("Select Id and Date");
        }
        private void cbStorageReportId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = (from store in Context.Stores
                         where store.Id == (int)cbStorageReportId.SelectedItem
                         select store.Name).FirstOrDefault();

            tbStoreName5.Text = query;
        }
        //Report 2
        private void button14_Click(object sender, EventArgs e)
        {


            if (cbReportProductId2.Text != "" && tbStorageReportDateF2.Text != "" && tbStorageReportDateT2.Text != "")
            {
                DateTime dateF = Convert.ToDateTime(tbStorageReportDateF2.Text);
                DateTime dateT = Convert.ToDateTime(tbStorageReportDateT2.Text);
                int[] selectedStore = checkedListBox1.CheckedItems.Cast<int>().ToArray();
                var query = (from supply in Context.Supply_Permit
                             join exchange in Context.Exchange_Permit
                             on new { supply.Product_Id, supply.Storage_Id } equals new { exchange.Product_Id, exchange.Storage_Id }
                             into currentGroup
                             from exchange in currentGroup.DefaultIfEmpty()
                             from prod in Context.Products
                             where supply.Product_Id == (int)cbReportProductId2.SelectedItem
                                    && supply.Product_Id == prod.Id
                                    && selectedStore.Contains((supply.Storage_Id).Value)
                                    && supply.Date >= dateF
                                    && supply.Date <= dateT
                             select new
                             {
                                 supply.Storage_Id,
                                 Storage_Name = supply.Store.Name,
                                 supply.Product_Id,
                                 prod.Name,
                                 Supply_Quantity = supply.Quantity,
                                 Supply_Date = supply.Date,
                                 supply.Production_Date,
                                 supply.Expiration_Date,
                                 Exchange_Quantity = exchange.Quantity,
                                 Exchange_Date = exchange.Date,
                                 Total_Quantity = supply.Quantity - (exchange.Quantity!=null ? exchange.Quantity : 0)
                             });


                if (query!= null && query.Count() > 0)
                {
                    dgvReport2.DataSource = query.ToList();
                }
                else
                {
                    MessageBox.Show("Wrong Data");
                }

            }
            else MessageBox.Show("Select Id and Date");
        }

        private void cbReportProductId2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            var storeList = (from store in Context.Supply_Permit
                             where store.Product_Id == (int)cbReportProductId2.SelectedItem
                             select new { store.Storage_Id }).Distinct();
            foreach (var store in storeList)
            {
                checkedListBox1.Items.Add(store.Storage_Id);
            }

            var query = (from product in Context.Supply_Permit
                         where product.Product_Id == (int)cbReportProductId2.SelectedItem
                         select product.Product.Name).FirstOrDefault();
            tbProductName4.Text = query;

        }


        //Report 3
        private void button15_Click(object sender, EventArgs e)
        {
            if (tbStorageReportDateF3.Text != "" && tbStorageReportDateT3.Text != "")
            {
                DateTime dateF = tbStorageReportDateF3.Value.Date;
                DateTime dateT = Convert.ToDateTime(tbStorageReportDateT3.Text);
                int[] selectedStore = checkedListBox2.CheckedItems.Cast<int>().ToArray();
                var query = (from supply in Context.Supply_Permit
                             join exchange in Context.Exchange_Permit
                             on new { supply.Product_Id, supply.Storage_Id } equals new { exchange.Product_Id, exchange.Storage_Id }
                             into currentGroup
                             from exchange in currentGroup.DefaultIfEmpty()
                             from prod in Context.Products
                             where supply.Product_Id == prod.Id
                                    && selectedStore.Contains((supply.Storage_Id).Value)
                                    && supply.Date >= dateF
                                    && supply.Date <= dateT
                             select new
                             {
                                 supply.Storage_Id,
                                 Storage_Name = supply.Store.Name,
                                 supply.Product_Id,
                                 prod.Name,
                                 Supply_Quantity = supply.Quantity,
                                 Supply_Date = supply.Date,
                                 Exchange_Quantity = exchange.Quantity,
                                 Exchange_Date = exchange.Date,
                                 Total_Quantity = supply.Quantity - (exchange.Quantity!=null ? exchange.Quantity : 0)
                             });


                if (query!= null && query.Count() > 0)
                {
                    dgvReport3.DataSource = query.ToList();
                }
                else
                {
                    MessageBox.Show("Wrong Data");
                }
            }
            else MessageBox.Show("Select Id and Date");


        }

        //Report 4
        private void button16_Click(object sender, EventArgs e)
        {

            int[] selectedStore = checkedListBox3.CheckedItems.Cast<int>().ToArray();
            var query = (from supply in Context.Supply_Permit
                         from prod in Context.Products
                         where supply.Product_Id == prod.Id
                                && selectedStore.Contains((supply.Storage_Id).Value)
        && supply.Expiration_Date <= DateTime.Today
                         select new
                         {
                             supply.Storage_Id,
                             Storage_Name = supply.Store.Name,
                             supply.Product_Id,
                             prod.Name,
                             supply.Quantity,
                             supply.Date
                         ,

                             Storage_time = DbFunctions.DiffDays(supply.Date, DateTime.Today)%30 + " Days " +
                                            DbFunctions.DiffMonths(supply.Date, DateTime.Today)%12 + " Months " +
                                            DbFunctions.DiffYears(supply.Date, DateTime.Today)+ " Years "
                         });


            if (query!= null && query.Count() > 0)
            {
                dgvReport4.DataSource = query.ToList();
            }
            else
            {
                MessageBox.Show("Select Id");
            }

        }


        //Report 5
        private void button17_Click(object sender, EventArgs e)
        {
            if (tbStorageReportDate4.Text != "")
            {
                int daysBeforeExpiration = Convert.ToInt32(tbStorageReportDate4.Text);
                DateTime daysToExpiration = DateTime.Today.AddDays(daysBeforeExpiration);
                int[] selectedStore = checkedListBox4.CheckedItems.Cast<int>().ToArray();
                var query = (from supply in Context.Supply_Permit
                             from prod in Context.Products
                             where supply.Product_Id == prod.Id
                                    && selectedStore.Contains((supply.Storage_Id).Value)
                                    && supply.Expiration_Date >= DateTime.Today
                                    && supply.Expiration_Date <= daysToExpiration
                             select new
                             {
                                 supply.Storage_Id,
                                 Storage_Name = supply.Store.Name,
                                 supply.Product_Id,
                                 prod.Name,
                                 supply.Quantity,
                                 supply.Date,
                                 Remains_time = DbFunctions.DiffDays(DateTime.Today, supply.Expiration_Date)%30 + " Days " +
                                                           DbFunctions.DiffMonths(DateTime.Today, supply.Expiration_Date)%12 + " Months " +
                                                           DbFunctions.DiffYears(DateTime.Today, supply.Expiration_Date)+ " Years "
                             });

                if (query!= null && query.Count() > 0)
                {
                    dgvReport5.DataSource = query.ToList();
                }
                else
                {
                    MessageBox.Show("Wrong Data");
                }
            }
            else MessageBox.Show("Select Id and Time");
        }


    }
}
