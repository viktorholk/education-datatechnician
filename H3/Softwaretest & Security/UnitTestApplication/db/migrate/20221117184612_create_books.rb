class CreateBooks < ActiveRecord::Migration[7.0]
  def change
    create_table :books do |t|
      t.string :title
      t.string :ISBN
      t.references :author, null: false, foreign_key: true
      t.integer :number_of_pages

      t.timestamps
    end
  end
end
